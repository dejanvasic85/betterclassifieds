using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Authorizes the user to the ad they are requesting only if it belongs to them
    /// </summary>
    public class AuthorizeBookingIdentity : ActionFilterAttribute
    {
        [Dependency]
        public ICategoryAdFactory CategoryAdFactory { get; set; }

        [Dependency]
        public IBookingManager BookingManager { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var adId = GetIdFromContext(filterContext);

            if (!adId.HasValue)
            {
                throw new BookingAuthorisationException("Parameter was not specified that would authorise the user to edit the ad");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new BookingAuthorisationException("User requesting Ad details is not authenticated");
            }

            // Fetch the booking to check if is belongs to the user
            var username = filterContext.HttpContext.User.Identity.Name;
            var adBooking = BookingManager.GetBooking(adId.Value);

            if (adBooking.UserId == username)
                return;

            if (adBooking.CategoryAdType.HasValue() &&
                CategoryAdFactory.CreateAuthoriser(adBooking.CategoryAdType)
                    .IsUserAuthorisedForAd(username, adBooking))
                return;

            throw new BookingAuthorisationException(
                $"User [{username}] attempted to retrieve ad [{adId}] that does not belong to them.");

        }

        private int? GetIdFromContext(ActionExecutingContext filterContext)
        {
            int id;

            if (filterContext.RouteData.Values.ContainsKey("id") && int.TryParse(filterContext.RouteData.Values["id"].ToString(), out id))
            {
                return id;
            }

            if (filterContext.ActionParameters.ContainsKey("id") && int.TryParse(filterContext.ActionParameters["id"].ToString(), out id))
            {
                return id;
            }

            return null;
        }
    }
}