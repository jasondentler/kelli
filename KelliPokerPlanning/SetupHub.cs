using KelliPokerPlanning.Models;
using SignalR.Hubs;

namespace KelliPokerPlanning
{
    public class SetupHub : Hub
    {
        private readonly IAccountManager _accountManager;

        public SetupHub(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public IsValidAndAvailableResult IsValidAndAvailable(string userName)
        {
            return new IsValidAndAvailableResult()
                       {
                           userName = userName,
                           isValid = IsValid(userName),
                           isAvailable = IsAvailable(userName)
                       };
        }

        private static bool IsValid(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName);
        }

        private static bool IsAvailable(string userName)
        {
            return userName.ToLowerInvariant() != "jason";
        }
    }
}