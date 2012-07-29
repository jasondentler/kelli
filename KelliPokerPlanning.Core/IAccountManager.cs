namespace KelliPokerPlanning
{
    public interface IAccountManager
    {

        Settings GetAccountSettings(string userName);
        string Create(string userName, string[] values, bool includeQuestionMark, bool includeInfinity);

    }
}
