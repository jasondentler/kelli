using System;
using System.Linq;

namespace KelliPokerPlanning
{
    public class PokerSetup
    {
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