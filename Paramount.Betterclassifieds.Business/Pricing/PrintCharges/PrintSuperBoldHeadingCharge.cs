using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintSuperBoldHeadingCharge : IPrintCharge
    {
        public AdCharge Calculate(RateModel rateModel, BookingCart booking)
        {
            Guard.NotNullIn(rateModel, booking, booking.LineAdModel);
            var price = booking.LineAdModel.IsSuperBoldHeading
                ? rateModel.LineAdSuperBoldHeading.GetValueOrDefault()
                : 0;
            return new AdCharge(price, "Print Super Bold Heading");
        }
    }
}