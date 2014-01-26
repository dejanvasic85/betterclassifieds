using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models.Seo;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface ISeoMappingRepository
    {
        void CreateCategoryMapping(string seoName, int categoryIds);
        void RetrieveMappings(string partition);

        SeoNameMappingModel GetCategoryMapping(string seoName);
    }
}