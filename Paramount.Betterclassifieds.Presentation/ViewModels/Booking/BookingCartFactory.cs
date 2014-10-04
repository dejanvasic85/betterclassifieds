﻿using System;
using System.Web;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;

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
            var cart = new BookingCart
            {
                SessionId = sessionId,
                UserId = username,
                Id = Guid.NewGuid().ToString(),
                OnlineAdCart = new OnlineAdCart()
            };

            bookingSessionIdentifier.SetId(cart.Id);

            return cart;
        }
    }
}