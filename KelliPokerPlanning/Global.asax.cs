using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;

namespace KelliPokerPlanning
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
                return;

            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }

            var userData = authTicket.UserData;
            var user = JsonConvert.DeserializeObject<User>(userData);

            if (user == null)
                return;

            Context.Items["user"] = user;

            // retrieve roles from UserData
            var roles = new string[0];
            const string estimator = "Estimator";
            const string moderator = "Moderator";

            switch (user.Role)
            {
                case KelliPokerPlanning.User.Roles.None:
                    break;
                case KelliPokerPlanning.User.Roles.Estimator:
                    roles = new[] {estimator};
                    break;
                case KelliPokerPlanning.User.Roles.Moderator:
                    roles = new[] {moderator};
                    break;
                case KelliPokerPlanning.User.Roles.Moderator | KelliPokerPlanning.User.Roles.Estimator:
                    roles = new[] {moderator, estimator};
                    break;
                default:
                    throw new ApplicationException("Invalid Role value");
            }


            if (Context.User != null)
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
        }

    }
}