using System.Web.Mvc;
using KelliPokerPlanning.Models;
using MvcContrib.Filters;
using MvcContrib;

namespace KelliPokerPlanning.Controllers
{

    public class EstimateController : Controller
    {
        private readonly ISessionManager _sessionManager;

        public EstimateController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public ActionResult Index(int? id, string joinCode)
        {
            PlanningSession session = null;
            if (id.HasValue && !string.IsNullOrWhiteSpace(joinCode))
                session = _sessionManager.JoinSession(id.Value, joinCode);

            if (session == null)
                return this.RedirectToAction(c => c.Join(id, joinCode));

            // id is the planning session id
            return Content("Hi!");
        }

        [HttpGet, ModelStateToTempData]
        public ViewResult Join(int? id, string joinCode)
        {
            return View(new Join()
                            {
                                SessionId = id,
                                JoinCode = joinCode
                            });
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Join(Join model)
        {
            PlanningSession session = null;
            if (ModelState.IsValid && model.SessionId.HasValue && !string.IsNullOrWhiteSpace(model.JoinCode))
                session = _sessionManager.JoinSession(model.SessionId.Value, model.JoinCode);
            if (session == null)
            {
                ModelState.AddModelError(" ", "Unable to find the session");
                return this.RedirectToAction(c => c.Join(model.SessionId, model.JoinCode));
            }
            return this.RedirectToAction(c => c.Index(model.SessionId, model.JoinCode));
        }

    }
}
