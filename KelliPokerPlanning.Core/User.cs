using System;

namespace KelliPokerPlanning
{
    public class User
    {

        public const string Moderator = "Moderator";
        public const string Estimator = "Estimator";

        [Flags]
        public enum Roles
        {
            Estimator = 0,
            Moderator = 1,
        }

        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string SiteApiName { get; set; }
        public int UserId { get; set; }
        public Roles Role { get; set; }

    }
}
