using System.Collections.Generic;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ListingsController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly SearchFilters _searchFilters;
        private const int ResultsPerPage = 5;
        private const int MaxPageRequests = 5;

        public ListingsController(ISearchService searchService, SearchFilters searchFilters)
        {
            _searchService = searchService;
            _searchFilters = searchFilters;
        }

        [HttpGet]
        public ActionResult Find(string keyword = "", int? categoryId = null, int? locationId = null)
        {
            // Set the search filters 
            _searchFilters.Keyword = keyword;
            _searchFilters.CategoryId = categoryId;
            _searchFilters.LocationId = locationId;

            var searchModel = new SearchModel
            {
                SearchResults = new List<AdSummaryViewModel>(),
                SearchFilters = _searchFilters
            };

            var adid = _searchFilters.AdId;
            if (adid.HasValue)
            {
                // The user has submitted an actual adid - so go straight there... 
                var ad = _searchService.GetAdById(adid.Value);
                if (ad == null)
                    return View(searchModel);

                return Redirect(Url.AdUrl(ad.TitleSlug, adid.Value));
            }

            // We should pass the filters to the search 
            // But this is just to get the front end cshtml files functioning
            var results = _searchService.Search();

            if (keyword.HasValue())
                results = results.Where(b => b.Description.Contains(keyword) || b.Title.Contains(keyword));

            results = results.OrderByDescending(b => b.AdId).Take(ResultsPerPage);

            // Map the results for the view model
            searchModel.SearchResults = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());
            
            // ViewBag.Title = "Search results...";
            ViewBag.ResultsPerPage = ResultsPerPage;
            ViewBag.MaxPageRequests = MaxPageRequests;
            
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ShowMore(PageRequest pageRequest)
        {
            var results = _searchService.Search()
                .OrderByDescending(b => b.AdId)
                .Skip(pageRequest.Page * ResultsPerPage)
                .Take(ResultsPerPage);

            var viewModel = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());

            // Return partial view
            return View("_ListingResults", viewModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("ListingsCtrlProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
        }

    }
}
