namespace Paramount.Betterclassifieds.Presentation
{
    using Microsoft.Practices.Unity;
    using Business.Booking;
    using System.Web.Mvc;


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

            if (filterContext.HttpContext.User == null || filterContext.HttpContext.User.Identity.Name.IsNullOrEmpty())
            {
                throw new BookingAuthorisationException("User is not logged in.");
            }

            if (bookingCart.UserId.IsNullOrEmpty())
            {
                throw new BookingAuthorisationException("Booking cart user id is null or empty");
            }

            if (bookingCart.UserId.DoesNotEqual(loggedInUser))
            {
                throw new BookingAuthorisationException(string.Format("Booking Cart does not belong to {0}. Cart belongs to {1}", loggedInUser, bookingCart.UserId));
            }
        }
    }
}