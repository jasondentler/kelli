using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using IronRuby.Builtins;
using KelliPokerPlanning.Models;
using MvcContrib.Filters;
using MvcContrib;

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
            var reqUrl = Request.Url;
            var baseUrl = new UriBuilder(reqUrl.Scheme, reqUrl.Host, reqUrl.Port).Uri;
            var channelUrl = new Uri(baseUrl, Url.Action<HomeController>(c => c.ChannelUrl()));

            return View(new Authentication()
                            {
                                ChannelUrl = channelUrl.ToString(),
                                ClientId = ConfigurationManager.AppSettings["StackExchangeClientId"],
                                Key = ConfigurationManager.AppSettings["StackExchangeKey"]
                            });
        }

        private ActionResult Auth()
        {
            throw new NotImplementedException();
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Index(Authentication model)
        {
            //if (!ModelState.IsValid)
            //    return this.RedirectToAction(c => c.Index());

            //if (_accountManager.IsAvailable(model.UserName))
            //    return this.RedirectToAction<SessionController>(c => c.Create(model.UserName));

            //return this.RedirectToAction<SessionController>(c => c.Index(model.UserName));

            throw new NotImplementedException();
        }

        public ActionResult ChannelUrl()
        {
            return Content(string.Empty, "text/html");
        }

        [HttpPost, ModelStateToTempData]
        public RedirectToRouteResult Authenticate(Authentication model)
        {

            var user = _accountManager.GetStackExchangeUser(
                model.SiteAPIName,
                model.UserId,
                model.AccessToken,
                ConfigurationManager.AppSettings["StackExchangeKey"],
                Request.Url.Host == "localhost");

            if (user == null)
            {
                ModelState.AddModelError(" ", "Unable to validate your account with Stack Exchange.");
                return this.RedirectToAction(c => c.Index());
            }

            Session["user"] = user;
            FormsAuthentication.RedirectFromLoginPage(user.DisplayName , false);

            return null;
        }
    }
}
