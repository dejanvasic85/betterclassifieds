﻿using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Models.Seo;

namespace Paramount.Betterclassifieds.Business.Search
{
    public interface ISearchService
    {
        IEnumerable<AdSearchResult> Search();
        List<CategorySearchResult> GetTopLevelCategories();
        List<LocationSearchResult> GetLocations();

        List<OnlineListingModel> SearchOnlineListing(string searchterm, IEnumerable<int> categoryIds,
            IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25, int order = 4);
        AdSearchResult GetAdById(int id);
        SeoNameMappingModel GetSeoMapping(string seoName);
    }
}