using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
        public ActionResult Index()
        {
            var user = (User)HttpContext.Items["user"];
            var settings = _accountManager.GetAccountSettings(user.SiteApiName, user.UserId);

            if (settings == null)
                return this.RedirectToAction(c => c.Create());

            return View(Mapper.Map<Settings, PokerSetup>(settings));
        }
        
        [HttpGet, ModelStateToTempData]
        public ActionResult Create()
        {
            var user = (User) HttpContext.Items["user"];
            var settings = _accountManager.GetAccountSettings(user.SiteApiName, user.UserId);

            if (settings != null)
                return this.RedirectToAction(c => c.Index());

            var values = new[] { "XS", "S", "M", "L", "XL", "2X" };
            return View(new PokerSetup()
            {
                Values = string.Join(Environment.NewLine, values),
                IncludeQuestion = true,
                IncludeInfinity = true
            });
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Create(PokerSetup model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Create());

            var user = (User)HttpContext.Items["user"];

            var values = (model.Values ?? "")
                .Split(Environment.NewLine.ToCharArray())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            var documentId = _accountManager.Create(user.SiteApiName, user.UserId, values, model.IncludeQuestion, model.IncludeInfinity);

            return this.RedirectToAction(c => c.Index());
        }

    }
}
