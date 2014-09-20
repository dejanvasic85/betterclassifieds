using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCartFactory
    {
        public static BookingCart Create(IUnityContainer container)
        {
            return CreateBookingCart(HttpContext.Current.Session.SessionID,
                HttpContext.Current.User.Identity.Name,
                container.Resolve<IBookingId>());
        }

        public static BookingCart CreateBookingCart(string sessionId, string username, IBookingId bookingId)
        {
            var cart = new BookingCart
            {
                SessionId = sessionId,
                UserId = username,
                Id = Guid.NewGuid().ToString(),
                OnlineAdCart = new OnlineAdCart()
            };

            bookingId.SetId(cart.Id);

            return cart;
        }
    }
}