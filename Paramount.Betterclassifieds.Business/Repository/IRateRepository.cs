using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IRateRepository
    {
        RateModel GetRatecard(int rateId);
        OnlineAdRate GetOnlineRateByCategory(int? categoryId);
    }
}