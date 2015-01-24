namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingCartRepository
    {
        BookingCart GetBookingCart(string id);
        BookingCart SaveBookingCart(BookingCart bookingCart);
    }
}