using AutoMapper;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Interface;
using Microsoft.Ajax.Utilities;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ListingsController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IBookingManager _bookingManager;
        private readonly SearchFilters _searchFilters;
        private readonly IClientConfig _clientConfig;

        private const string Tempdata_ComingFromSearch = "IsComingFromSearch";

        public ListingsController(ISearchService searchService, SearchFilters searchFilters, IClientConfig clientConfig,
            IBookingManager bookingManager)
        {
            _searchService = searchService;
            _searchFilters = searchFilters;
            _clientConfig = clientConfig;
            _bookingManager = bookingManager;
        }

        [HttpGet]
        public ActionResult Find(string keyword = "", int? categoryId = null, int? locationId = null, int? sort = null)
        {
            // Set the search filters that should be available (in session) all the time
            _searchFilters.Keyword = keyword;
            _searchFilters.CategoryId = categoryId;
            _searchFilters.LocationId = locationId;

            if (sort.HasValue)
            {
                _searchFilters.Sort = sort.Value;
            }
            else if (keyword.HasValue())
            {
                _searchFilters.Sort = (int)AdSearchSortOrder.MostRelevant;
            }

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
            var results = _searchService.GetAds(_searchFilters.Keyword, _searchFilters.CategoryId, _searchFilters.LocationId, index: 0, pageSize: _clientConfig.SearchResultsPerPage, order: (AdSearchSortOrder)_searchFilters.Sort);

            if (results.Count > 0)
            {
                searchModel.TotalCount = results.First().TotalCount;
            }

            // Map the results for the view model
            searchModel.SearchResults = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());

            ViewBag.Title = string.Format("Classified Search | {0}", searchModel.SearchSummary);
            ViewBag.AllowUserToFetchMore = searchModel.TotalCount > _clientConfig.SearchResultsPerPage;
            ViewBag.ResultsPerPage = _clientConfig.SearchResultsPerPage;
            ViewBag.MaxPageRequests = _clientConfig.SearchMaxPagedRequests;
            TempData[Tempdata_ComingFromSearch] = true;
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ShowMore(PageRequest pageRequest)
        {
            var results = _searchService.GetAds(_searchFilters.Keyword, _searchFilters.CategoryId,
                _searchFilters.LocationId, index: pageRequest.Page, pageSize: _clientConfig.SearchResultsPerPage,
                order: (AdSearchSortOrder)_searchFilters.Sort);

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

            _searchFilters.FromSeoMapping(seoMapping);

            var results = _searchService.GetAds(seoMapping.SearchTerm, seoMapping.CategoryIds,
                seoMapping.LocationIds, seoMapping.AreaIds, index: 0, pageSize: _clientConfig.SearchResultsPerPage);

            // Map the results for the view model
            searchModel.SearchResults = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());
            searchModel.SearchFilters = _searchFilters;

            if (results.Count > 0)
            {
                searchModel.TotalCount = results.First().TotalCount;
            }

            ViewBag.Title = "Classifieds for Tutors";
            ViewBag.AllowUserToFetchMore = searchModel.TotalCount > _clientConfig.SearchResultsPerPage;
            ViewBag.ResultsPerPage = _clientConfig.SearchResultsPerPage;
            ViewBag.MaxPageRequests = _clientConfig.SearchMaxPagedRequests;

            return View("Find", searchModel);
        }

        [HttpGet]
        public ActionResult ViewAd(int id, string titleSlug = "")
        {
            // Remember - Id is always AdBookingId
            var adSearchResult = _searchService.GetAdById(id);

            if (adSearchResult == null || adSearchResult.HasExpired() || adSearchResult.HasNotStarted())
            {
                // Ad doesn't exist so return 404
                return View("404");
            }

            // Increment the hits ( the retrieve was successfull )
            _bookingManager.IncrementHits(id);

            // Map to the view model now
            var adViewModel = this.Map<AdSearchResult, AdViewModel>(adSearchResult);

            if (adViewModel.OnlineAdTag.HasValue())
                adViewModel.TutorAd = this.Map<TutorAdModel, TutorAdView>(_searchService.GetTutorAd(id));

            ViewBag.IsComingFromSearch = false;
            if (TempData[Tempdata_ComingFromSearch] != null)
            {
                ViewBag.IsComingFromSearch = (bool)TempData[Tempdata_ComingFromSearch];
                ViewBag.BackToSearchUrl = Url.Action("Find", _searchFilters);
            }
            ViewBag.Title = adViewModel.Heading;

            return View(adViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, CaptchaVerify("Captcha is not valid")]
        public ActionResult AdEnquiry(AdEnquiryViewModel adEnquiry)
        {
            if (!ModelState.IsValid)
            {
                IUpdateInfoModel updatedCaptcha = this.GenerateCaptchaValue(5);
                return Json(new
                {
                    isValid = false,
                    imageElementId = updatedCaptcha.ImageUrl,
                    tokenElementId = updatedCaptcha.TokenValue
                });
            }

            var enquiry = this.Map<AdEnquiryViewModel, Business.Models.AdEnquiry>(adEnquiry);
            _bookingManager.SubmitAdEnquiry(enquiry);

            return Json(new{ isValid = true });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("ListingsCtrlProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
            configuration.CreateMap<AdSearchResult, AdViewModel>()
                .ForMember(member => member.Price, options => options.Condition(v => v.Price > 0));
            configuration.CreateMap<TutorAdModel, TutorAdView>();
            configuration.CreateMap<AdEnquiryViewModel, Business.Models.AdEnquiry>();
        }

    }
}
