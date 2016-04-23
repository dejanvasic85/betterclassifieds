using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation
{
    public class EnsurePaymentNotInProgressAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IEventBookingContext EventBookingContext { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (EventBookingContext?.EventBookingId != null)
            {
                filterContext.Result = new Redirector().MakeTicketPayment(); ;
            }
        }
    }
}