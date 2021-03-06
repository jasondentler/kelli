﻿using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KelliPokerPlanning.Models;
using MvcContrib.Filters;
using MvcContrib;
using Newtonsoft.Json;

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
        
        public ActionResult ChannelUrl()
        {
            return Content(string.Empty, "text/html");
        }

        [HttpPost, ModelStateToTempData]
        public ActionResult ModeratorAuthenticate(Authentication model)
        {

            User user = null;

            user = _accountManager.GetStackExchangeUser(
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

            new AuthorizationHelper().SetAuthenticationCookie(HttpContext, user, KelliPokerPlanning.User.Roles.Moderator);

            return this.RedirectToAction<SessionController>(c => c.List());
        }
    }
}
