namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookCartRepository
    {
        BookingCart GetBookingCart(string id);
        IBookingCart Save(IBookingCart bookingCart);
    }
    
}