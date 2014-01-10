using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Repository
{
    using Models;

    public interface IAdRepository
    {
        TutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(TutorAdModel tutorAdModel);
        List<OnlineAdModel> GetLatestAds(int takeLast = 10);
    }
}