using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public class OnlineBasePriceCharge : IOnlineChargeableItem
    {
        private const string LineItemName = "Listing Fee";

        public OnlineChargeItem Calculate(OnlineAdRate rate, OnlineAdModel onlineAdModel)
        {
            Guard.NotNull(rate);

            var price = onlineAdModel == null ? 0 : rate.MinimumCharge;

            return new OnlineChargeItem(price, LineItemName);
        }
    }
}