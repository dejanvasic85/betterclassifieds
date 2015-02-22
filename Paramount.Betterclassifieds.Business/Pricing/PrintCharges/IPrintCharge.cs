namespace Paramount.Betterclassifieds.Business
{
    using Booking;

    public interface IPrintCharge
    {
        AdCharge Calculate(RateModel rateModel, BookingCart booking);
    }
}