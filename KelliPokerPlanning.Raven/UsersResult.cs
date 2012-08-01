using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KelliPokerPlanning
{
    public class UsersResult
    {
        public UserResult[] items { get; set; }
    }

    public class UserResult
    {
        public int user_id { get; set; }
        public string display_name { get; set; }
        public string profile_image { get; set; }
    }

}
