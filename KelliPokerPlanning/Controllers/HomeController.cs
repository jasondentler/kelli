using System;
using System.Web.Mvc;
using KelliPokerPlanning.Models;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountManager _accountManager;

        public HomeController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet, ModelStateToTempData]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Index(Authentication model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Index());

            if (_accountManager.IsAvailable(model.UserName))
                return this.RedirectToAction<SessionController>(c => c.Create(model.UserName));

            return this.RedirectToAction<SessionController>(c => c.Index(model.UserName));
        }

    }
}
