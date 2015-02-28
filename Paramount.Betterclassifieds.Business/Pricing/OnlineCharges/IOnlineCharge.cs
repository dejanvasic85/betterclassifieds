namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    public interface IOnlineCharge
    {
        AdChargeItem Calculate(OnlineAdRate rate, OnlineAdModel onlineAdModel);
    }
}