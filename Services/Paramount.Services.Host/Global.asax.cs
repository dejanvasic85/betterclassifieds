using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Principal;
using System.Text;
using Paramount.ApplicationBlock.Configuration;
using Paramount.ApplicationBlock.Logging.DataAccess;

namespace Paramount.Services.Host
{
    public class Global : System.Web.HttpApplication, IServiceInformation
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.StatusCode == 401)
            {
                //if the status is 401 the WWW-Authenticated is added to
                //the response so client knows it needs to send credentials

                HttpContext context = HttpContext.Current;
                context.Response.StatusCode = 401;
                context.Response.AddHeader("WWW-Authenticate", "Basic Realm");
            }
        }

        public override void Init()
        {
            base.Init();
            //this.AuthenticateRequest += OnAuthenticateRequest;
            //EndRequest += OnEndRequest;
        }

        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var context = (HttpApplication)sender;

            //the Authorization header is checked if present
            string authHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader))
            {
                string authStr = context.Request.Headers["Authorization"];

                if (authStr == null || authStr.Length == 0)
                {
                    // No credentials; anonymous request
                    return;
                }

                authStr = authStr.Trim();
                if (authStr.IndexOf("Basic", 0) != 0)
                {
                    //header not correct we do not authenticate
                    return;
                }

                authStr = authStr.Trim();
                string encodedCredentials = authStr.Substring(6);
                byte[] decodedBytes = Convert.FromBase64String(encodedCredentials);
                string s = new ASCIIEncoding().GetString(decodedBytes);

                string[] userPass = s.Split(new char[] { ':' });
                string username = userPass[0];

                //the user is validated against the SqlMemberShipProvider
                //If it is validated then the roles are retrieved from the
                //role provider and a generic principal is created
                //the generic principal is assigned to the user context
                // of the application

                //if (Membership.ValidateUser(username, password))
                if (true)
                {
                    //string[] roles = Roles.GetRolesForUser(username);
                    context.Context.User = new GenericPrincipal(
                        new GenericIdentity(username), new string[1]);
                }
            }
        }

        private void DenyAccess(HttpApplication context)
        {
            context.Response.StatusCode = 401;
            context.Response.StatusDescription = "Access Denied";

            // error not authenticated
            context.Response.Write("401 Access Denied");
            context.CompleteRequest();
        }

        public string ApplicationName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
    }
}