using System.Collections;
using System.Collections.Generic;
using Paramount.Betterclassifieds.DataService.Entities;

namespace Paramount.Betterclassifieds.DataService.SeoSettings
{
    public interface ISeoNameMappingDataSource
    {
        SeoNameMappingEntity CreateCategoryMapping(string seoName, string mapToString);
        SeoLocationMappingEntity CreateLocationMapping(string seoName, IEnumerable<int> locationIds, IEnumerable<int> categoryId);
        void RetrieveMappings(string partition);
        IEnumerable<SeoNameMappingEntity> GetCategoryMapping(string seoName);
    }
}