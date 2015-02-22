namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IRateRepository
    {
        RateModel GetRatecard(int rateId);
        RateModel[] GetRatesForPublicationCategory(int[] publications, int? subCategoryId);
        OnlineAdRate GetOnlineRateForCategories(params int?[] categories);
    }
}