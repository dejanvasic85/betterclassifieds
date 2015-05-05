namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingContext
    {
        BookingCart Current();
        BookingCart NewFromTemplate(AdBookingModel adBookingTemplate);
        bool IsAvailable();
        void Clear();
    }
}