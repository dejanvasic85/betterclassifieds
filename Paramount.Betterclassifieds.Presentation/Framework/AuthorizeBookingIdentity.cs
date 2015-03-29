using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Authorizes the user to the ad they are requesting only if it belongs to them
    /// </summary>
    public class AuthorizeBookingIdentity : ActionFilterAttribute
    {
        [Dependency]
        public IBookingManager BookingManager { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int adId;
            
            if (!int.TryParse(filterContext.ActionParameters["id"].ToString(), out adId))
            {
                throw new BookingAuthorisationException("Ad ID is not a valid integer");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new BookingAuthorisationException("User requesting Ad details is not authenticated");
            }

            // Fetch the booking to check if is belongs to the user
            var username = filterContext.HttpContext.User.Identity.Name;
            if (!BookingManager.AdBelongsToUser(adId, username))
            {
                throw new BookingAuthorisationException(string.Format("User [{0}] attempted to retrieve ad [{1}] that does not belong to them.", username, adId));
            }
        }
    }
}