using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Raven.Client;

namespace KelliPokerPlanning
{

    public class AccountManager : IAccountManager 
    {
        private readonly IDocumentSession _session;

        public AccountManager(IDocumentSession session)
        {
            _session = session;
        }
        
        public Settings GetAccountSettings(string siteApiName, int userId)
        {
            return _session.Query<Settings>()
                .SingleOrDefault(s => s.SiteApiName == siteApiName && s.UserId == userId);
        }

        public string Create(string siteApiName, int userId, string[] values, bool includeQuestionMark, bool includeInfinity)
        {
            var settings = new Settings()
                               {
                                   SiteApiName = siteApiName,
                                   UserId = userId,
                                   Values = values ?? new string[0],
                                   IncludeQuestionMark = includeQuestionMark,
                                   IncludeInfinity = includeInfinity
                               };
            _session.Store(settings);
            return settings.Id;
        }
        
        public User GetStackExchangeUser(string siteApiName, int userId, string accessToken, string key, bool isLocal)
        {

            if (isLocal)
                return new User()
                           {
                               AvatarUrl =
                                   "http://www.gravatar.com/avatar/2aaf05c5e05389c501b4fd7451abecdb?d=identicon&r=PG",
                               DisplayName = "Jason Dentler",
                               SiteApiName = "stackoverflow",
                               UserId = 837001
                           };

            const string url = "https://api.stackexchange.com/2.0/me";
            var parms = new Dictionary<string, string>()
                            {
                                {"access_token", accessToken},
                                {"filter", "default"},
                                {"key", ConfigurationManager.AppSettings["StackExchangeKey"]},
                                {"site", siteApiName},
                                {"order", "desc"},
                                {"sort", "reputation"}
                            };

            var uri = BuildUri(url, parms);

            var req = (HttpWebRequest) WebRequest.Create(uri);

            req.Accept = "application/json, text/javascript, */*; q=0.01";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            var data = GetResponse(req);

            Debug.WriteLine(data);

            Trace.WriteLine(data);

            if (string.IsNullOrWhiteSpace(data))
                return null;

            var results = JsonConvert.DeserializeObject<UsersResult>(data);

            if (results == null)
                return null;

            var user = results.items.SingleOrDefault(i => i.user_id == userId);

            if (user == null)
                return null;

            return new User()
                       {
                           AvatarUrl = user.profile_image,
                           DisplayName = user.display_name,
                           SiteApiName = siteApiName,
                           UserId = user.user_id
                       };

        }

        private Uri BuildUri(string url, Dictionary<string, string> parms)
        {
            var pairs = parms.Select(kv => string.Format("{0}={1}",
                                                         HttpUtility.UrlEncode(kv.Key),
                                                         HttpUtility.UrlEncode(kv.Value)));
            var queryString = string.Join("&", pairs);
            return new UriBuilder(url)
                       {
                           Query = queryString
                       }.Uri;
        }

        private string GetResponse(HttpWebRequest request)
        {
            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

    }    


}