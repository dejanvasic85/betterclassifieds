namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingContext
    {
        BookingCart FetchOrCreate();
        BookingCart Current();
        BookingCart Create(AdBookingModel adBookingTemplate);
        bool IsAvailable();
        void Clear();
    }
}