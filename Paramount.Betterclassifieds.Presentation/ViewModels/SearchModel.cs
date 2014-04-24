using System.Collections.Generic;
using System.Linq;

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
        public bool HasResults { get { return SearchResults.Any(); } }

        // Filters
        public SearchFilters SearchFilters { get; set; }
    }
}