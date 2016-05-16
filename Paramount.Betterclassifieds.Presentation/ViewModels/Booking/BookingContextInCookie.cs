using System.Monads;
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
        private const string BookingCookieName = "bookingCookie";
        private readonly IBookCartRepository _repository;
        private readonly IClientConfig _clientConfig;
        private readonly HttpContextBase _httpContext;
        private readonly ILogService _logService;

        public BookingContextInCookie(IBookCartRepository repository, IClientConfig clientConfig, 
            HttpContextBase httpContext, ILogService logService)
        {
            _repository = repository;
            _clientConfig = clientConfig;
            _httpContext = httpContext;
            _logService = logService;
        }

        public BookingCart Current()
        {
            var booking = _repository.GetBookingCart(Id);

            if (booking == null)
            {
                _logService.Info("Booking created");
                booking = new BookingCart(_httpContext.With(c => c.Session).SessionID, _httpContext.User.Identity.Name);
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
            var booking = BookingCart.Create(_httpContext.With(c => c.Session).SessionID, _httpContext.User.Identity.Name, adBookingTemplate, _clientConfig);

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
                var httpCookie = _httpContext.Request.Cookies[BookingCookieName];
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
                var bookingCookie = new HttpCookie(BookingCookieName);
                bookingCookie.Value = value.HasValue()
                    ? CryptoHelper.Encrypt(value)
                    : value;

                _httpContext.Response.Cookies.Add(bookingCookie);
            }
        }

        public override string ToString()
        {
            return Current().Id;
        }
    }
}