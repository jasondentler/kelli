using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace KelliPokerPlanning
{
    public class SessionManager : ISessionManager
    {
        private readonly IDocumentSession _session;
        private readonly JoinCodeGenerator _joinCodeGenerator;

        public SessionManager(IDocumentSession session, JoinCodeGenerator joinCodeGenerator)
        {
            _session = session;
            _joinCodeGenerator = joinCodeGenerator;
        }
        
        public PokerSetup GetSetup(User user)
        {
            return _session.Query<PlanningSession>()
                .Where(ps => ps.SiteApiName == user.SiteApiName && ps.UserId == user.UserId)
                .OrderByDescending(ps => ps.Start)
                .Select(ps => ps.Setup)
                .FirstOrDefault();
        }

        public IEnumerable<SessionListItem> GetSessions(User user)
        {
            return _session.Query<PlanningSession>()
                .Where(ps => ps.SiteApiName == user.SiteApiName && ps.UserId == user.UserId)
                .Where(ps => ps.End == null)
                .Select(ps => new SessionListItem()
                {
                    Id = ps.Id,
                    Start = ps.Start,
                    EstimateCount = ps.Estimates.Count
                })
                .ToList();
        }

        public PlanningSession ResumeSession(User user, int sessionId)
        {
            var session = _session.Load<PlanningSession>(sessionId) ?? new PlanningSession();
            if (session.SiteApiName == user.SiteApiName && session.UserId == user.UserId)
                return session;

            throw new ApplicationException(string.Format("Session {0} not found for {1} user {2}",
                                                         sessionId, user.SiteApiName, user.UserId));
        }

        public PlanningSession StartNewSession(User user, PokerSetup setup)
        {
            var session = new PlanningSession()
                              {
                                  SiteApiName = user.SiteApiName,
                                  UserId = user.UserId,
                                  Estimates = new List<Estimate>(),
                                  Start = DateTime.UtcNow,
                                  Setup = setup,
                                  JoinCode = _joinCodeGenerator.Generate()
                              };
            _session.Store(session);
            return session;
        }

        public void EndSession(User user, int sessionId)
        {
            var session = ResumeSession(user, sessionId);
            session.End = DateTime.UtcNow;
            _session.Store(session);
        }
    }
}
