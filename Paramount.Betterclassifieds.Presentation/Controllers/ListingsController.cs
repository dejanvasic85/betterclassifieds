using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{

    public class ListingsController : ApplicationController, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IBookingManager _bookingManager;
        private readonly SearchFilters _searchFilters;
        private readonly IClientConfig _clientConfig;
        private readonly IAuthManager _authManager;
        private readonly IMailService _mailService;
        private readonly IApplicationConfig _appConfig;
        private readonly IRobotVerifier _robotVerifier;
        private readonly IUserManager _userManager;

        private const string Tempdata_ComingFromSearch = "IsComingFromSearch";

        public ListingsController(ISearchService searchService, SearchFilters searchFilters, IClientConfig clientConfig, IBookingManager bookingManager, IAuthManager authManager, IMailService mailService, IApplicationConfig appConfig, IRobotVerifier robotVerifier, IUserManager userManager)
        {
            _searchService = searchService;
            _searchFilters = searchFilters;
            _clientConfig = clientConfig;
            _bookingManager = bookingManager;
            _authManager = authManager;
            _robotVerifier = robotVerifier;
            _userManager = userManager;
            _appConfig = appConfig;
            _mailService = mailService.Initialise(this);
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
                var ad = _searchService.GetByAdId(adid.Value);
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

            ViewBag.Title = $"Classified Search | {searchModel.SearchSummary}";
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
            var adSearchResult = _searchService.GetByAdId(id);
            
            if (adSearchResult == null || adSearchResult.HasExpired() || adSearchResult.HasNotStarted())
            {
                // Ad doesn't exist so return 404
                return View("404");
            }

            // Increment the hits ( the retrieve was successfull )
            _bookingManager.IncrementHits(id);

            // Map to the view model now
            var adViewModel = this.Map<AdSearchResult, AdViewModel>(adSearchResult);
            if (_authManager.IsUserIdentityLoggedIn(User))
            {
                adViewModel.VisitorIsTheOwner = adSearchResult.Username == User.Identity.Name;
            }

            ViewBag.IsComingFromSearch = false;
            if (TempData[Tempdata_ComingFromSearch] != null)
            {
                ViewBag.IsComingFromSearch = (bool)TempData[Tempdata_ComingFromSearch];
                ViewBag.BackToSearchUrl = Url.Action("Find", _searchFilters);
            }
            ViewBag.Title = adViewModel.Heading;
            ViewBag.FacebookAppId = _clientConfig.FacebookAppId;

            return View(adViewModel);
        }

        [HttpPost]
        public ActionResult AdEnquiry(AdEnquiryViewModel adEnquiry, string googleCaptchaResult)
        {
            if (!ModelState.IsValid)
            {
                return JsonModelErrors();
            }

            var currentUser = _userManager.GetCurrentUser();
            if (currentUser != null)
            {
                adEnquiry.FullName = currentUser.FullName;
                adEnquiry.Email = currentUser.Email;
                adEnquiry.Phone = currentUser.Phone; 
            }
            else
            {
                if (adEnquiry.FullName.IsNullOrWhiteSpace())
                {
                    ModelState.AddModelError("FullName", "Full name is required");
                    return JsonModelErrors();
                }
                if (adEnquiry.Email.IsNullOrWhiteSpace())
                {
                    ModelState.AddModelError("Email", "Email is required");
                    return JsonModelErrors();
                }

                if (!_robotVerifier.IsValid(_appConfig.GoogleAdEnquiryCatpcha.Secret, googleCaptchaResult))
                {
                    AddModelErrorCaptchaFailed();
                    return JsonModelErrors();
                }
            }
            
            var enquiry = this.Map<AdEnquiryViewModel, AdEnquiry>(adEnquiry);
            _bookingManager.SubmitAdEnquiry(enquiry);
            _mailService.SendListingEnquiryEmail(adEnquiry);

            return Json(new { isValid = true });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("ListingsCtrlProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
            configuration.CreateMap<AdSearchResult, AdViewModel>()
                .ForMember(member => member.Price, options => options.Condition(v => v.Price > 0));
            configuration.CreateMap<AdEnquiryViewModel, AdEnquiry>();
        }

    }
}
