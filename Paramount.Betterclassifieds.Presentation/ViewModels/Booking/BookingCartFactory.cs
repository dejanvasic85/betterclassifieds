using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCartFactory
    {
        public static BookingCart Create(IUnityContainer container)
        {
            return CreateBookingCart(HttpContext.Current.Session.SessionID,
                HttpContext.Current.User.Identity.Name,
                container.Resolve<IBookingSessionIdentifier>());
        }

        public static BookingCart CreateBookingCart(string sessionId, string username, IBookingSessionIdentifier bookingSessionIdentifier)
        {
            var id = Guid.NewGuid().ToString();
            var cart = new BookingCart
            {
                SessionId = sessionId,
                UserId = username,
                Id = id,
                Reference = id.Substring(0, 6).ToUpper(),
                OnlineAdCart = new OnlineAdCart(),
                LineAdModel = new LineAdModel()
            };

            bookingSessionIdentifier.SetId(cart.Id);

            return cart;
        }
    }
}