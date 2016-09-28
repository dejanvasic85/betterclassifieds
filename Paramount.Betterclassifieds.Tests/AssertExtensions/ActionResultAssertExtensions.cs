using System.Web.Mvc;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests
{
    public static class ActionResultAssertExtensions
    {
        public static void IsRedirectingTo(this ActionResult actionResult, string expectedUrlPath)
        {
            var redirectResult = actionResult.IsTypeOf<RedirectResult>();
            expectedUrlPath.IsEqualTo(expectedUrlPath, ignoreCase: true);
        }

        public static void IsRedirectingTo(this ActionResult actionResult, string controller, string action)
        {
            var redirectResult = actionResult.IsTypeOf<RedirectToRouteResult>();
            redirectResult.RouteValues["controller"].ToString().IsEqualTo(controller, ignoreCase: true);
            redirectResult.RouteValues["action"].ToString().IsEqualTo(action, ignoreCase: true);
        }

        public static TExpected ViewResultModelIsTypeOf<TExpected>(this ActionResult actionResult)
        {
            return actionResult.IsTypeOf<ViewResult>().IsTypeOf<TExpected>();
        }
    }
}