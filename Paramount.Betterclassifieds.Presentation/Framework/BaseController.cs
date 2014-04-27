using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation
{
    public  abstract class BaseController : Controller
    {
        protected readonly ISearchService _searchService;

        protected BaseController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // Todo - legacy integration removal
        [HttpPost]
        public ActionResult Search(string searchKeyword, int? searchCategoryId)
        {
            SearchFilters filters = new SearchFilters { Keyword = searchKeyword};
            var adId = filters.AdId;
            if (adId.HasValue)
            {
                var ad = _searchService.GetAdById(adId.Value);
                Redirect(Url.AdUrl(ad.HeadingSlug, adId.Value));
            }

            OnlineSearchParam["SearchKeywordParam"] = searchKeyword;
            OnlineSearchParam["CategoryIdParam"] = searchCategoryId;

            return Redirect(LegacyIntegration.LegacyLinks.SearchResults);
        }


        // Todo - legacy integration removal
        // Currently this is legacy integration
        // So just set the session parameter to the search and redirect...
        // todo - legacy integration
        private Dictionary<string, object> OnlineSearchParam
        {
            get
            {
                Dictionary<string, object> sessionSearchParam =
                    Session["OnlineSearchParameter"] as Dictionary<string, object>;

                if (sessionSearchParam == null)
                {
                    sessionSearchParam = new Dictionary<string, object>();
                    Session["OnlineSearchParameter"] = sessionSearchParam;
                }

                return sessionSearchParam;
            }
        }

        public bool IsUserLoggedIn()
        {
            return this.User != null && this.User.Identity.IsAuthenticated;
        }
    }
}