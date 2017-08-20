using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class ListingQuery
    {
        public ListingQuery()
        {
            PageSize = 10;
        }
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
        public string User { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
        public int? PageNumber { get; set; }
    }
}