using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Decorator for the booking steps specific for category type and if not set, redirects to the first step
    /// </summary>
    public class BookingCategoryTypeRequired : ActionFilterAttribute
    {

        public BookingCategoryTypeRequired(string categoryAdType)
        {
            this.CategoryAdType = categoryAdType;
        }

        public string CategoryAdType { get; set; }

        [Dependency]
        public IBookingContext CurrentBookingContext { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var categoryAdTypeInContext = CurrentBookingContext.Current().CategoryAdType;
            if (categoryAdTypeInContext.IsNullOrEmpty())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "booking"},
                    {"action", "Step1"}
                });
            }

            if (categoryAdTypeInContext != this.CategoryAdType)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "booking"},
                    {"action", "Step2"},
                    {"adType", categoryAdTypeInContext }
                });
            }
        }
    }
}