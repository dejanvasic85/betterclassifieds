using Paramount.Betterclassifieds.Business.Booking;
using System;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    /// <summary>
    /// Manages the booking cart with the user's cookie
    /// </summary>
    public class BookingContextInCookie : IBookingContext
    {
        private readonly IBookCartRepository _repository;

        public BookingContextInCookie(IBookCartRepository repository)
        {
            _repository = repository;
        }

        public BookingCart Current()
        {
            var booking = _repository.GetBookingCart(Id);

            if (booking == null)
            {
                booking = new BookingCart(HttpContext.Current.Session.SessionID, HttpContext.Current.User.Identity.Name);
                _repository.Save(booking);
                Id = booking.Id;
            }

            return booking;
        }

        public bool IsAvailable()
        {
            return Id.HasValue();
        }

        private string Id
        {
            get
            {
                var httpCookie = HttpContext.Current.Request.Cookies["bookingCookie"];
                if (httpCookie == null)
                    return string.Empty;
                return httpCookie.Values["id"];
            }
            set
            {
                var cookie = new HttpCookie("bookingCookie") { Expires = DateTime.Now.AddDays(14) };
                cookie["id"] = value;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public override string ToString()
        {
            return Current().Id;
        }
    }
}