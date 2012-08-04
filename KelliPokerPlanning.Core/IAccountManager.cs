namespace KelliPokerPlanning
{
    public interface IAccountManager
    {
        User GetStackExchangeUser(string siteApiName, int userId, string accessToken, string key, bool isLocal);
    }
}
