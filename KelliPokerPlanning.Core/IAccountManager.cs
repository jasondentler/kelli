﻿namespace KelliPokerPlanning
{
    public interface IAccountManager
    {

        Settings GetAccountSettings(string siteApiName, int userId);
        string Create(string siteApiName, int userId, string[] values, bool includeQuestionMark, bool includeInfinity);
        bool IsValid(string userName);
        bool IsAvailable(string userName);

        User GetStackExchangeUser(string siteApiName, int userId, string accessToken, string key, bool isLocal);
    }
}
