using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace KelliPokerPlanning
{
    public class AuthorizationHelper
    {

        public void SetAuthenticationCookie(HttpContextBase context, User user, User.Roles newRoles)
        {
            user.Role = user.Role | newRoles;

            var ticket = new FormsAuthenticationTicket(
                1,
                user.DisplayName,
                DateTime.Now,
                DateTime.Now.AddDays(0.5),
                true,
                JsonConvert.SerializeObject(user));

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            context.Response.Cookies.Add(authCookie);
        }

        public void DecodeAuthenticationCookie(HttpContextBase context)
        {
            var authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
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

            context.Items["user"] = user;

            // retrieve roles from UserData
            string[] roles;

            switch (user.Role)
            {
                case KelliPokerPlanning.User.Roles.Estimator:
                    roles = new[] { User.Estimator };
                    break;
                case KelliPokerPlanning.User.Roles.Moderator:
                    roles = new[] { User.Moderator, User.Estimator };
                    break;
                default:
                    throw new ApplicationException("Invalid Role value");
            }

            if (context.User != null)
                context.User = new GenericPrincipal(context.User.Identity, roles);

        }

    }
}