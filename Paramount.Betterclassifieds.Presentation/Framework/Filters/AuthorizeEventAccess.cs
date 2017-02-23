using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation
{
    public class AuthorizeEventAccess : ActionFilterAttribute
    {
        [Dependency]
        public EventAccess EventAccess { get; set; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var eventId = GetIdFromContext(context);

            if (!eventId.HasValue)
                throw new EventAuthorisationException("EventId parameter was not supplied and is required");

            var username = context.HttpContext.User.Identity.Name;
            if (!EventAccess.IsUserAuthorisedForEvent(username, eventId.Value))
                throw new EventAuthorisationException($"User {username} does not have access to event {eventId}");
        }

        private int? GetIdFromContext(ActionExecutingContext filterContext)
        {
            int id;
            const string paramName = "eventId";

            if (filterContext.RouteData.Values.ContainsKey(paramName) && int.TryParse(filterContext.RouteData.Values[paramName].ToString(), out id))
            {
                return id;
            }

            if (filterContext.ActionParameters.ContainsKey(paramName) && int.TryParse(filterContext.ActionParameters[paramName].ToString(), out id))
            {
                return id;
            }

            return null;
        }
    }
}