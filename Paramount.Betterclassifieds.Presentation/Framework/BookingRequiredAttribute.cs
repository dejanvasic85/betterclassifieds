using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Ensures access only if the booking is in progress otherwise redirects to the first step
    /// </summary>
    public class BookingRequiredAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IBookingContext CurrentBookingId { get; set; }

        [Dependency]
        public IBookingCartRepository Repository { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!CurrentBookingId.IsAvailable())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "booking"},
                    {"action", "Step1"}
                });
            }
        }
    }
}