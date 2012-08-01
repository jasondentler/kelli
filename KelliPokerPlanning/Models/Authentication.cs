using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelliPokerPlanning.Models
{
    public class Authentication
    {

        public string ClientId { get; set; }
        public string Key { get; set; }
        public string ChannelUrl { get; set; }

        public string AccessToken { get; set; }
        public string SiteName { get; set; }
        public int UserId { get; set; }

    }
}