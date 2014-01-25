namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface ISeoMappingRepository
    {
        void CreateCategoryMapping(string seoName, int categoryIds);
    }
}