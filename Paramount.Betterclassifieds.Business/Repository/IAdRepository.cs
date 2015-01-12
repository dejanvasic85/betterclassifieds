namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IAdRepository
    {
        void CreateAdEnquiry(AdEnquiry adEnquiryTemplate);
        string GetAdvertiserEmailForAd(int adId);
        OnlineAdModel GetOnlineAd(int adId);
        void UpdateOnlineAd(OnlineAdModel onlineAd);
    }
}