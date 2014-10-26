using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Authorizes the user to the ad they are requesting only if it belongs to them.
    /// </summary>
    public class AuthorizeCartIdentity : ActionFilterAttribute
    {
        [Dependency]
        public IBookingManager BookingManager { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var bookingCart = BookingManager.GetCart();

            if (bookingCart == null)
            {
                throw new BookingAuthorisationException("Booking cart is not available");
            }

            var loggedInUser = filterContext.HttpContext.User.Identity.Name;
            if (bookingCart.UserId.IsNullOrEmpty() || bookingCart.UserId.DoesNotEqual(loggedInUser))
            {
                throw new BookingAuthorisationException(string.Format("Booking Cart does not belong to {0}", loggedInUser));
            }
        }
    }
}