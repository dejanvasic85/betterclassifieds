using System.Web.Mvc;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests
{
    public static class ActionResultAssertExtensions
    {
        public static void IsRedirectingTo(this ActionResult actionResult, string expected)
        {
            var redirectResult = (RedirectResult)actionResult;
            Assert.That(redirectResult.Url, Is.EqualTo(expected));
        }
        
        public static TExpected ViewResultModelIsTypeOf<TExpected>(this ActionResult actionResult)
        {
            var viewResult = (ViewResult)actionResult;
            var model = viewResult.Model;
            Assert.That(model, Is.TypeOf<TExpected>());
            return (TExpected)model;
        }

        public static void RedirectResultControllerIs(this RedirectToRouteResult result, string expectedController)
        {
            Assert.That(result.RouteValues["controller"], Is.EqualTo(expectedController));
        }

        public static void RedirectResultActionIs(this RedirectToRouteResult result, string expectedAction)
        {
            Assert.That(result.RouteValues["action"], Is.EqualTo(expectedAction));
        }

        public static void RedirectResultIsNotFound(this RedirectToRouteResult result)
        {
            RedirectResultControllerIs(result, "Error");
            RedirectResultActionIs(result, "NotFound");
        }
    }
}