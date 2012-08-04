using System;
using System.Collections.Generic;

namespace KelliPokerPlanning
{
    public class PlanningSession
    {
        public int Id { get; set; }
        public string SiteApiName { get; set; }
        public int UserId { get; set; }
        public PokerSetup Setup { get; set; }
        public string JoinCode { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public List<Estimate> Estimates { get; set; }
    }
}
