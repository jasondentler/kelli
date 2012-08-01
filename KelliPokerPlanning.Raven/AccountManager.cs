using System;
using System.Collections.Generic;
using System.Configuration;
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
        
        public Settings GetAccountSettings(string userName)
        {
            userName = userName.ToLowerInvariant();

            return _session.Query<Settings>().SingleOrDefault(s => s.UserName == userName);
        }

        public string Create(string userName, string[] values, bool includeQuestionMark, bool includeInfinity)
        {
            var settings = new Settings()
                               {
                                   UserName = userName.ToLowerInvariant(),
                                   Values = values ?? new string[0],
                                   IncludeQuestionMark = includeQuestionMark,
                                   IncludeInfinity = includeInfinity
                               };

            _session.Store(settings);
            return settings.Id;
        }

        public bool IsValid(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName);
        }

        public bool IsAvailable(string userName)
        {
            userName = userName.ToLowerInvariant();
            return !_session.Query<Settings>().Any(s => s.UserName == userName);
        }

        public User GetStackExchangeUser(string siteApiName, int userId, string accessToken, string key, bool isLocal)
        {

            if (isLocal)
                return new User()
                           {
                               AvatarUrl = "http://www.gravatar.com/avatar/2aaf05c5e05389c501b4fd7451abecdb?d=identicon&r=PG",
                               DisplayName = "Jason Dentler",
                               SiteAPIName = "stackoverflow",
                               UserId = 837001
                           };

            var url = "https://api.stackexchange.com/2.0/me";
            var parms = new Dictionary<string, string>()
                            {
                                {"access_token", accessToken},
                                {"filter", "default"},
                                {"key", ConfigurationManager.AppSettings["StackExchangeKey"]},
                                {"site", siteApiName},
                                {"order", "desc"},
                                {"sort", "reputation"}
                            };

            var wc = new WebClient();
            wc.Headers[HttpRequestHeader.Accept] = "application/json, text/javascript, */*; q=0.01";

            parms.ToList().ForEach(kv => wc.QueryString[kv.Key] = kv.Value);
            var data = wc.DownloadString(url);

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
                SiteAPIName = siteApiName,
                UserId = user.user_id
            };

        }

    }    


}