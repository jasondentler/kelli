using System;
using System.Linq;
using System.Web.Mvc;
using KelliPokerPlanning.Models;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class SessionController : Controller
    {
        private readonly IAccountManager _accountManager;

        public SessionController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet, ModelStateToTempData]
        public ViewResult Index()
        {
            return View(model:User.Identity.Name);
        }
        
        [HttpGet, ModelStateToTempData]
        public ViewResult Create(string id)
        {
            var user = (User) HttpContext.Items["user"];
            var settings = _accountManager.GetAccountSettings(user.SiteApiName, user.UserId);

            if (settings != null)
            {
                return View(new PokerSetup()
                                {
                                    UserName = settings.UserName,
                                    Values = string.Join(Environment.NewLine, settings.Values),
                                    IncludeQuestion = settings.IncludeQuestionMark,
                                    IncludeInfinity = settings.IncludeInfinity
                                });
            }

            var values = new[] { "XS", "S", "M", "L", "XL", "2X" };
            return View(new PokerSetup()
            {
                UserName = id,
                Values = string.Join(Environment.NewLine, values),
                IncludeQuestion = true,
                IncludeInfinity = true
            });
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Create(PokerSetup model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Create(model.UserName));

            var values = (model.Values ?? "")
                .Split(Environment.NewLine.ToCharArray())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            var documentId = _accountManager.Create(model.UserName, values, model.IncludeQuestion, model.IncludeInfinity);

            return this.RedirectToAction(c => c.Index());
        }

    }
}
