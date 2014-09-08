using System;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    // todo - Move this in to a lifetime managed object for Unity. So that we don't keep calling GetId
    public class BookingCartSessionManager
    {
        public string GetId()
        {
            // Fetch the Id from the cookie
            return BookingCookie["id"];
        }
        
        private HttpCookie BookingCookie
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies["bookingCookie"];
                if (cookie == null)
                    cookie = new HttpCookie("bookingCookie") { Expires = DateTime.Now.AddDays(14) };

                HttpContext.Current.Response.Cookies.Add(cookie);

                return cookie;
            }
        }
    }
}