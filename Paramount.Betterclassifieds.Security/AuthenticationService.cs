using System;
using System.Web;
using System.Web.Security;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.Security
{
    public class AuthenticationService : IAuthManager
    {
        private readonly IApplicationConfig _applicationConfig;
        private const string ForceSSLCookieName = "ClassifiedsSSL";

        public AuthenticationService(IApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public void Login(string username, bool createPersistentCookie, string role = "User")
        {
            HttpContext context = HttpContext.Current;

            // Login via Forms Auth
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: username,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddYears(100),
                isPersistent: createPersistentCookie,
                userData: role,
                cookiePath: FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = _applicationConfig.UseHttps
            };

            context.Response.Cookies.Add(cookie);

            if (_applicationConfig.UseHttps)
            {
                // Drop a second cookie indicating that the user is logged in via SSL (no secret data, just tells us to redirect them to SSL)
                context.Response.Cookies.Add(new HttpCookie(ForceSSLCookieName, "true"));
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();

            // Delete the "LoggedIn" cookie
            HttpContext context = HttpContext.Current;
            var cookie = context.Request.Cookies[ForceSSLCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1d);
                context.Response.Cookies.Add(cookie);
            }
        }

        public bool ValidatePassword(string username, string password)
        {
            // Use the existing built-in membership provider for now
            return Membership.ValidateUser(username, password);
        }

        public void CreateMembership(string username, string password)
        {
            Membership.CreateUser(username, password, email: username);
        }
    }
}
