namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IRateRepository
    {
        RateModel GetRatecard(int rateId);
        OnlineAdRate GetOnlineRateForCategories(params int?[] categories);
    }
}