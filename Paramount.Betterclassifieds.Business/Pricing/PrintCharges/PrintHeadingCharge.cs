using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintHeadingCharge : IPrintCharge
    {
        public AdCharge Calculate(RateModel rateModel, BookingCart booking)
        {
            Guard.NotNullIn(rateModel, booking, booking.LineAdModel);
            var price = booking.LineAdModel.AdHeader.HasValue()
                ? rateModel.BoldHeading.GetValueOrDefault()
                : 0;
            return new AdCharge(price, "Print Heading");
        }
    }
}