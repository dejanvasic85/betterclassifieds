using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class FindModel
    {
        public FindModel()
        {
            SearchResults = new List<AdSummaryViewModel>();
            SearchFilters = new SearchFilters();
            SortByOptions = AdSearchSortOrder.MostRelevant;
        }

        // Results
        public List<AdSummaryViewModel> SearchResults { get; set; }
        public bool HasResults { get { return SearchResults.Any(); } }

        // Filters
        public SearchFilters SearchFilters { get; set; }
        public AdSearchSortOrder SortByOptions { get; set; }
    }
}