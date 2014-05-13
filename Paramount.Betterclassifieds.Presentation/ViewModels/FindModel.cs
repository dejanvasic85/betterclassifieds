using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class FindModel
    {
        public FindModel()
        {
            SearchResults = new List<AdSummaryViewModel>();
            SearchFilters = new SearchFilters();
        }

        public List<AdSummaryViewModel> SearchResults { get; set; }

        public bool HasResults { get { return SearchResults.Any(); } }

        // Filters
        public SearchFilters SearchFilters { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        /// Displays to the user what we have found e.g There are 15 ads that matched your search
        /// </summary>
        public string SearchSummary
        {
            get 
            {
                if (!HasResults)
                    return string.Empty;

                string searchSummary = string.Format("Found {0} results", TotalCount);

                if (SearchFilters.Keyword.HasValue())
                    searchSummary = searchSummary.Append(string.Format(" for '{0}'", SearchFilters.Keyword));

                return searchSummary;
            }
        }

    }
}