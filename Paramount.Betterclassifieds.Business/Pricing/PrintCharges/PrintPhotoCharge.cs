namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    public class PrintPhotoCharge : IPrintCharge
    {
        public AdCharge Calculate(RateModel rateModel, BookingCart booking)
        {
            Guard.NotNullIn(rateModel, booking, booking.LineAdModel);

            var price = booking.LineAdModel.AdImageId.HasValue()
                ? rateModel.PhotoCharge.GetValueOrDefault()
                : 0;

            return new AdCharge(price, "Print Photo");
        }
    }
}