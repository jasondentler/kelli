using SignalR.Hubs;

namespace KelliPokerPlanning
{
    public class SetupHub : Hub
    {

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

    public class IsValidAndAvailableResult
    {
        public string userName { get; set; }
        public bool isValid { get; set; }
        public bool isAvailable { get; set; }
    }

}