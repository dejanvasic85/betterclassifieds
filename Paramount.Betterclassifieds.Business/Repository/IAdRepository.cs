using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IAdRepository
    {
        TutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(TutorAdModel tutorAdModel);
        void CreateAdEnquiry(AdEnquiry adEnquiryTemplate);
        string GetAdvertiserEmailForAd(int adId);
        OnlineAdModel GetOnlineAd(int adId);
        void UpdateOnlineAd(OnlineAdModel onlineAd);
    }
}