using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Search
{
    public interface ISearchService
    {
        IEnumerable<AdSearchResult> Search();

        List<CategorySearchResult> GetTopLevelCategories();

        List<LocationSearchResult> GetLocations();
    }
}