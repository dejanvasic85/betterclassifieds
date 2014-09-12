using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    /// <summary>
    /// View model representing what can be selected in a regular booking steps/stages
    /// </summary>
    public class BookingCart
    {
        public string SessionId { get; set; }

        public string Id { get; set; }

        public bool Completed { get; set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int[] Publications { get; set; }

        public BookingCart()
        {
            Publications = new int[] { };
        }

        public bool IsStep1NotValid()
        {
            return !CategoryId.HasValue || !SubCategoryId.HasValue;
        }
    }


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
                Id = Guid.NewGuid().ToString()
            };

            bookingId.SetId(cart.Id);

            return cart;
        }
    }
}