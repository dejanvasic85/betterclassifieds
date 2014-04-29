using System.Collections.Generic;
using AutoMapper;
using Microsoft.Ajax.Utilities;
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
        private const int ResultsPerPage = 10;
        private const int MaxPageRequests = 10;

        public ListingsController(ISearchService searchService, SearchFilters searchFilters)
        {
            _searchService = searchService;
            _searchFilters = searchFilters;
        }

        [HttpGet]
        public ActionResult Find(string keyword = "", int? categoryId = null, int? locationId = null, int sort = 4)
        {
            // Set the search filters 
            _searchFilters.Keyword = keyword;
            _searchFilters.CategoryId = categoryId;
            _searchFilters.LocationId = locationId;

            var searchModel = new FindModel
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

                return Redirect(Url.AdUrl(ad.HeadingSlug, adid.Value));
            }

            // We should pass the filters to the search 
            // But this is just to get the front end cshtml files functioning
            var results = _searchService.GetAds(_searchFilters.Keyword, _searchFilters.CategoryId, _searchFilters.LocationId, index: 0, pageSize: ResultsPerPage, order: (AdSearchSortOrder)_searchFilters.Sort);

            // Map the results for the view model
            searchModel.SearchResults = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());

            ViewBag.Title = "Search results for classies";
            ViewBag.ResultsPerPage = ResultsPerPage;
            ViewBag.MaxPageRequests = MaxPageRequests;
            ViewBag.SortOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Newest First", Value = "0"},
                new SelectListItem { Text = "Oldest First", Value = "1"},
                new SelectListItem { Text = "Lowest Price", Value = "2"},
                new SelectListItem { Text = "Highest Price", Value = "3"},
                new SelectListItem { Selected = true, Text = "Most Relevant", Value = "4"},
            };

            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ShowMore(PageRequest pageRequest)
        {
            var results = _searchService.GetAds(_searchFilters.Keyword, _searchFilters.CategoryId,
                _searchFilters.LocationId, index: pageRequest.Page, pageSize: ResultsPerPage,
                order: (AdSearchSortOrder) _searchFilters.Sort);

            var viewModel = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());

            // Return partial view
            return View("_ListingResults", viewModel);
        }

        [HttpGet]
        public ActionResult SeoAds(string seoName)
        {
            // Setup the view model
            var searchModel = new FindModel
            {
                SearchFilters = _searchFilters.Clear(),
                SearchResults = new List<AdSummaryViewModel>()
            };

            // Display the search page with the SEO driving the filters
            if (seoName.IsNullOrWhiteSpace())
            {
                // Simply show no results
                return View("Find", searchModel);
            }

            // Fetch the SEO details
            var seoMapping = _searchService.GetSeoMapping(seoName);
            if (seoMapping == null)
            {
                return View("Find", searchModel);
            }

            _searchFilters.ApplySeoMapping(seoMapping);

            var results = _searchService.GetAds(seoMapping.SearchTerm, seoMapping.CategoryIds,
                seoMapping.LocationIds, seoMapping.AreaIds, index: 0, pageSize: ResultsPerPage);

            // Map the results for the view model
            searchModel.SearchResults = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());
            searchModel.SearchFilters = _searchFilters;

            return View("Find", searchModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("ListingsCtrlProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
        }

    }
}
