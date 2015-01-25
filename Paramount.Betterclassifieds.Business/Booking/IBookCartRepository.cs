namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookCartRepository
    {
        BookingCart GetBookingCart(string id);
        BookingCart Save(BookingCart bookingCart);
    }
    
}