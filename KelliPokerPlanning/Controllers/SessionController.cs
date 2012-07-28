using System;
using System.Web.Mvc;
using KelliPokerPlanning.Models;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    public class SessionController : Controller
    {

        [HttpGet, ModelStateToTempData]
        public ViewResult Index(string id)
        {
            var values = new[] { "XS", "S", "M", "L", "XL", "2X" };
            return View(new PokerSetup()
            {
                Id = id,
                Values = string.Join(Environment.NewLine, values),
                IncludeQuestion = true,
                IncludeInfinity = true
            });
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Index(PokerSetup model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Index(model.Id));
            return this.RedirectToAction(c => c.Index(model.Id));
        }

    }
}
