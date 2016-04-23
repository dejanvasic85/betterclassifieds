using System.Monads;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation
{
    public class EventBookingContextRequiredAttribute : ActionFilterAttribute
    {
        public string RequiredProperty { get; }

        public EventBookingContextRequiredAttribute(string requiredProperty)
        {
            RequiredProperty = requiredProperty;
        }

        [Dependency]
        public IEventBookingContext EventBookingContext { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var notFoundResult = new Redirector().NotFound();

            if (EventBookingContext == null)
            {
                filterContext.Result = notFoundResult;
            }

            var prop = typeof(EventBookingContext).GetProperty(this.RequiredProperty);

            var value = prop.GetValue(EventBookingContext).With(b => b.ToString());

            if (value.IsNullOrEmpty())
            {
                filterContext.Result = notFoundResult;
            }
        }
    }
}