using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Repository
{
    using Models;

    public interface IAdRepository
    {
        TutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(TutorAdModel tutorAdModel);
        List<OnlineAdModel> GetLatestAds(int takeLast = 10);
        List<OnlineAdModel> GetOnlineAdsByCategory(List<int> categoryIds, int index = 0, int pageSize = 25);
    }
}