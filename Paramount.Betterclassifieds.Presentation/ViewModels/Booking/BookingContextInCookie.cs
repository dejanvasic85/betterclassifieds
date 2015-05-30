using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using System.Web;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    /// <summary>
    /// Manages the booking cart with the user's cookie
    /// </summary>
    public class BookingContextInCookie : IBookingContext
    {
        private readonly IBookCartRepository _repository;
        private readonly IClientConfig _clientConfig;
        private readonly HttpContextBase _httpContext;

        public BookingContextInCookie(IBookCartRepository repository, IClientConfig clientConfig, HttpContextBase httpContext)
        {
            _repository = repository;
            _clientConfig = clientConfig;
            _httpContext = httpContext;
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
                var httpCookie = _httpContext.Request.Cookies["bookingCookie"];
                if (httpCookie == null)
                {
                    return string.Empty;
                }
                
                try
                {
                    return CryptoHelper.Decrypt(httpCookie.Value);
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                var cookie = new HttpCookie("bookingCookie") { Value = CryptoHelper.Encrypt(value) };
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        public override string ToString()
        {
            return Current().Id;
        }
    }
}