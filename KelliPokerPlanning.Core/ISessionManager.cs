using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KelliPokerPlanning
{
    public interface ISessionManager
    {
        PokerSetup GetSetup(User user);
        IEnumerable<SessionListItem> GetSessions(User user);
        PlanningSession ResumeSession(User user, int sessionId);
        PlanningSession StartNewSession(User user, PokerSetup setup);
        void EndSession(User user, int sessionId);
    }
}
