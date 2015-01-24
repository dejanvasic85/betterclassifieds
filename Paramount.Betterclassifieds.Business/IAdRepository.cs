using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public interface IAdRepository
    {
        void CreateAdEnquiry(AdEnquiry adEnquiryTemplate);
        string GetAdvertiserEmailForAd(int adId);
        OnlineAdModel GetOnlineAd(int adId);
        void UpdateOnlineAd(OnlineAdModel onlineAd);
    }
}