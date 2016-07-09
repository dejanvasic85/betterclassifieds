using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount.Betterclassifieds.Presentation
{
    public class Redirector
    {
        public ActionResult NotFound()
        {
            return new RedirectToRouteResult(Create("NotFound", "Error"));
        }

        public ActionResult MakeTicketPayment()
        {
            return new RedirectToRouteResult(Create("MakePayment", "Event"));
        }

        public ActionResult BookingStepOne()
        {
            return new RedirectToRouteResult(Create("Step1", "Booking"));
        }

        private RouteValueDictionary Create(string action, string controller)
        {
            return new RouteValueDictionary
            {
                { "controller", controller },
                { "action", action },
            };
        }
    }
}