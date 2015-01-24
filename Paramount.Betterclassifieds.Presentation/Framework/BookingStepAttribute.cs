using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Ensures that the user does not attempt access to a step ahead of the process
    /// </summary>
    public class BookingStepAttribute : ActionFilterAttribute
    {
        public BookingStepAttribute(int stepNumber)
        {
            StepNumber = stepNumber;
        }

        public int? StepNumber { get; set; }

        [Dependency]
        public IBookingContext BookingContext { get; set; }

        [Dependency]
        public IBookingCartRepository BookingCartRepository { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (StepNumber == 1)
                return;

            if (!BookingContext.IsAvailable())
            {
                // There was never a booking so redirect to step 1
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Booking"},
                    {"action", "Step1"}
                });
                return;
            }

            // Do not allow them to view this screen, so instead route them to the next step to complete
            var lastCompleted = BookingContext.Current().GetLastCompletedStepNumber();

            var nextStep = lastCompleted + 1;

            if (StepNumber > nextStep)
            {
                // Redirect to the 
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Booking"},
                    {"action", string.Format("Step{0}", nextStep)}
                });
            }
        }
    }
}