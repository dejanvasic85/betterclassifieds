namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingContext
    {
        BookingCart Current();
        bool IsAvailable();
    }
}