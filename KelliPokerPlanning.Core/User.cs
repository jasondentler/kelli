using System;

namespace KelliPokerPlanning
{
    public class User 
    {
        [Flags]
        public enum Roles
        {
            None = 0,
            Moderator = 1,
            Estimator = 2
        }

        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string SiteApiName { get; set; }
        public int UserId { get; set; }
        public Roles Role { get; set; }

    }
}
