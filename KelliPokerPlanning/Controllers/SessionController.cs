using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class SessionController : Controller
    {
        private readonly ISessionManager _sessionManager;

        public SessionController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        [HttpGet, ModelStateToTempData]
        public ActionResult Index(int id)
        {
            var user = (User)HttpContext.Items["user"];
            var session = _sessionManager.ResumeSession(user, id);
            return View(session);
        }
        
        [HttpGet, ModelStateToTempData]
        public ActionResult List()
        {
            var user = (User) HttpContext.Items["user"];
            var sessions = _sessionManager.GetSessions(user);

            if (!sessions.Any())
                return this.RedirectToAction(c => c.Create());

            return View(sessions);
        }

        [HttpGet, ModelStateToTempData]
        public ActionResult Create()
        {
            var values = new[] {"XS", "S", "M", "L", "XL", "2X"};

            var user = (User) HttpContext.Items["user"];
            var setup = _sessionManager.GetSetup(user)
                        ?? new PokerSetup()
                               {
                                   Values = string.Join(Environment.NewLine, values),
                                   IncludeQuestion = true,
                                   IncludeInfinity = true
                               };

            return View(setup);
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Create(PokerSetup model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Create());

            var user = (User)HttpContext.Items["user"];

            var session = _sessionManager.StartNewSession(user, model);

            return this.RedirectToAction(c => c.Index(session.Id));
        }

    }
}
