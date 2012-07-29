using System;

namespace KelliPokerPlanning.Models
{
    public class PokerSetup
    {
        public string UserName { get; set; }
        public string Values { get; set; }
        public bool IncludeQuestion { get; set; }
        public bool IncludeInfinity { get; set; }
    }
}