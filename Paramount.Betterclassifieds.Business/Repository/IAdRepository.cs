using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IAdRepository
    {
        TutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(TutorAdModel tutorAdModel);
        List<OnlineAdModel> GetOnlineAdsByCategory(List<int> categoryIds, int index = 0, int pageSize = 25);
    }
}