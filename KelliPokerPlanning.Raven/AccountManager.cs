using System.Linq;
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

    }    


}