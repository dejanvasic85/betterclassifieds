using System.Monads;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation
{
    public class EventBookingRequiredAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IEventBookingContext EventBookingContext { get; set; }

        public bool AllowCompleted { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var redirectResult = new UrlHelper(HttpContext.Current.Request.RequestContext)
                .EventBookingSessionExpired()
                .ToRedirectResult();

            if (EventBookingContext?.EventBookingId == null)
            {
                filterContext.Result = redirectResult;
            }

            if (EventBookingContext.With(b => b.EventBookingComplete) && !AllowCompleted)
            {
                filterContext.Result = redirectResult;
            }
        }
    }
}