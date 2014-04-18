using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Search
{
    public interface ISearchService
    {
        IEnumerable<AdSearchResult> Search();
    }
}