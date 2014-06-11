using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Search
{
    public interface ISearchService
    {
        List<AdSearchResult> GetAds(string searchterm, IEnumerable<int> categoryIds, IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25, AdSearchSortOrder order = AdSearchSortOrder.MostRelevant);
        List<AdSearchResult> GetAds(string searchterm, int? categoryId, int? locationId, int index = 0, int pageSize = 25, AdSearchSortOrder order = AdSearchSortOrder.MostRelevant);
        List<AdSearchResult> GetLatestAds(int pageSize = 10);
        List<CategorySearchResult> GetTopLevelCategories();
        List<LocationSearchResult> GetLocations();
        AdSearchResult GetAdById(int id);
        SeoNameMappingModel GetSeoMapping(string seoName);
        TutorAdModel GetTutorAd(int id);
    }
}