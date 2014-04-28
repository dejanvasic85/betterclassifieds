using Paramount.Betterclassifieds.Business.Search;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation
{
    public  abstract class BaseController : Controller
    {
        protected readonly ISearchService _searchService;

        protected BaseController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        
        public bool IsUserLoggedIn()
        {
            return this.User != null && this.User.Identity.IsAuthenticated;
        }
    }
}