using System;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCookie : IBookingId
    {
        public string Id { get { return GetCookie()["id"]; } }

        public void SetId(string value)
        {
            GetCookie().Values["id"] = value;
        }

        private HttpCookie GetCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies["bookingCookie"];
            if (cookie == null)
                cookie = new HttpCookie("bookingCookie") { Expires = DateTime.Now.AddDays(14) };

            HttpContext.Current.Response.Cookies.Add(cookie);

            return cookie;

        }

        public override string ToString()
        {
            return Id;
        }
    }

    public interface IBookingId
    {
        string Id { get; }
        void SetId(string value);
    }
}