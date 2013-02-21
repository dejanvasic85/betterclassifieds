namespace Paramount.Common.UI.Handlers
{
    using System.Web;
    using Common.UIController;
    using Common.UIController.Contants;
    using Utility.Security;

    public class UnSubscribeHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var queryString = new SecureQueryString(context.Request.QueryString[QueryStringKeys.UnsubscribedDataKey]);
            var username = queryString[QueryStringKeys.Username];
            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            UserAccountProfileController.UnSubscribeUser(username);
            context.Response.Write("You have unsubscribed from iFlog Newsletter");
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
