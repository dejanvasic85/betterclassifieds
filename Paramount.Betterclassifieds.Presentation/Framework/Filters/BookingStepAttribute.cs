using System.Web.Mvc;
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
        public IBookCartRepository BookCartRepository { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (StepNumber == 1)
                return;

            if (!BookingContext.IsAvailable())
                filterContext.Result = new Redirector().BookingStepOne();
        }
    }
}