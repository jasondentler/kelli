using System;
using System.Linq;

namespace KelliPokerPlanning.Models
{
    public class PokerSetup
    {
        public string TeamName { get; set; }
        public string Values { get; set; }
        public bool IncludeQuestion { get; set; }
        public bool IncludeInfinity { get; set; }

        public string[] SplitValues()
        {
            return Values.Split(Environment.NewLine.ToCharArray())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }
    }
}