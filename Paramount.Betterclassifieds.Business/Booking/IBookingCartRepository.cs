namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IBookingCartRepository
    {
        BookingCart GetBookingCart(string id);
        BookingCart SaveBookingCart(BookingCart bookingCart);
    }
}