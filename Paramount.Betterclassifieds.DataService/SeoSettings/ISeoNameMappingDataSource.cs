namespace Paramount.Betterclassifieds.Business.Bookings.SeoSettings
{
    public interface ISeoNameMappingDataSource
    {
        object CreateCategoryMapping(string seoName, string mapToString);
    }
}