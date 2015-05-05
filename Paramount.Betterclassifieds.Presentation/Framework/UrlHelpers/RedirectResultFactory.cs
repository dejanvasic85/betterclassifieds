using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount
{
    public class RedirectResultFactory
    {
        public static RedirectToRouteResult Create(string action, string controller)
        {
            return new RedirectToRouteResult(new RouteValueDictionary
            {
                {"controller", controller},
                {"action", action}
            });
        }
    }
}