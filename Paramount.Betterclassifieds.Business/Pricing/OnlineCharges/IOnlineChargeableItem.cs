namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    public interface IOnlineChargeableItem
    {
        AdChargeItem Calculate(OnlineAdRate rate, OnlineAdModel onlineAdModel);
    }
}