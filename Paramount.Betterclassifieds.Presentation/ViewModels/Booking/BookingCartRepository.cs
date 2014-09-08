using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCartRepository
    {
        public BookingCart GetBookingCart(string id)
        {
            return HttpContext.Current.Session["bookingSession"] as BookingCart;
        }

        public BookingCart SaveBookingCart(BookingCart bookingCart)
        {
            HttpContext.Current.Session["bookingSession"] = bookingCart;
            return bookingCart;
        }
    }
}