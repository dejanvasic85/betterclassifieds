using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCartRepository
    {
        public BookingCart GetBookingCart(string id)
        {
            return HttpContext.Current.Session["booking"] as BookingCart;
        }

        public BookingCart SaveBookingCart(BookingCart bookingCart)
        {
            HttpContext.Current.Session["booking"] = bookingCart;
            return bookingCart;
        }
    }
}