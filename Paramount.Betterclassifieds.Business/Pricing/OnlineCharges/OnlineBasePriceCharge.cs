using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public class OnlineBasePriceCharge : IOnlineCharge
    {
        private const string LineItemName = "Online Ad";

        public AdCharge Calculate(OnlineAdRate rate, BookingCart booking)
        {
            Guard.NotNullIn(rate, booking);

            var price = booking.OnlineAdModel == null ? 0 : rate.MinimumCharge;

            return new AdCharge(price, LineItemName);
        }
    }
}