using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class SearchModel
    {
        public SearchModel()
        {
            SearchResults = new List<AdSummaryViewModel>();
            SearchFilters = new SearchFilters();
        }

        // Results
        public List<AdSummaryViewModel> SearchResults { get; set; }

        // Filters
        public SearchFilters SearchFilters { get; set; }
    }
}