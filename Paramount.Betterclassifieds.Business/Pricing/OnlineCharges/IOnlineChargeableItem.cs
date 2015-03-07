namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    public interface IOnlineChargeableItem
    {
        OnlineChargeItem Calculate(OnlineAdRate rate, OnlineAdModel onlineAdModel);
    }
}