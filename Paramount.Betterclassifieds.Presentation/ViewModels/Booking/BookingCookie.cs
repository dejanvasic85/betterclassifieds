using System;
using System.Web;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCookie : IBookingSessionIdentifier
    {
        public BookingCookie(IBookingCartRepository bookingManager)
        {
            
        }

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

    
}