using Paramount.Betterclassifieds.Business;
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
        private readonly IClientConfig _clientConfig;

        public BookingContextInCookie(IBookCartRepository repository, IClientConfig clientConfig)
        {
            _repository = repository;
            _clientConfig = clientConfig;
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

        /// <summary>
        /// Creates a new booking cart based on an existing ad
        /// </summary>
        public BookingCart NewFromTemplate(AdBookingModel adBookingTemplate)
        {
            var booking = BookingCart.FromBooking(HttpContext.Current.Session.SessionID,
                HttpContext.Current.User.Identity.Name,
                adBookingTemplate,
                _clientConfig);

            this.Id = booking.Id;
            _repository.Save(booking);

            return booking;
        }

        public bool IsAvailable()
        {
            return Id.HasValue();
        }

        public void Clear()
        {
            this.Id = null;
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