using System;
using System.Web.Mvc;
using KelliPokerPlanning.Models;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    public class HomeController : Controller
    {

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
            return this.RedirectToAction<SessionController>(c => c.Index(model.UserName));
        }

    }
}
