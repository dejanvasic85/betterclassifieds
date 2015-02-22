using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintWordCharge : IPrintCharge
    {
        public AdCharge Calculate(RateModel rateModel, BookingCart booking)
        {
            Guard.NotNullIn(rateModel, booking, booking.LineAdModel);
            var wordCount = booking.LineAdModel.AdText.Split(' ').Length;
            var price = rateModel.RatePerWord.GetValueOrDefault()*wordCount;
            return new AdCharge(price, string.Format("Print Words ({0})", wordCount));
        }
    }
}