using System;
using System.Web.Mvc;
using KelliPokerPlanning.Models;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet, ModelStateToTempData]
        public ViewResult Index()
        {
            var values = new[] {"XS", "S", "M", "L", "XL", "2X"};
            return View(new PokerSetup()
                            {
                                Values = string.Join(Environment.NewLine, values),
                                IncludeQuestion = true,
                                IncludeInfinity = true
                            });
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Index(PokerSetup model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Index());
            return this.RedirectToAction(c => c.Index());
        }

    }
}
