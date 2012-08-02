using System;

namespace KelliPokerPlanning
{
    public class Settings
    {

        public string Id { get; set; }
        public string SiteApiName { get; set; }
        public int UserId { get; set; }
        public string[] Values { get; set; }
        public bool IncludeQuestionMark { get; set; }
        public bool IncludeInfinity { get; set; }

        public string JoinValues()
        {
            return string.Join(Environment.NewLine, Values);
        }

    }
}