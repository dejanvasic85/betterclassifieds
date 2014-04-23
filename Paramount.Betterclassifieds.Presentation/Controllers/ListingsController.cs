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
        private const int ResultsPerPage = 5;
        private const int MaxPageRequests = 5;

        public ListingsController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public ActionResult Find(string keyword = "", int? categoryId = null, int? locationId = null)
        {
            // We should pass the filters to the search 
            // But this is just to get the front end cshtml files functioning
            var results = _searchService.Search();

            if (keyword.HasValue())
                results = results.Where(b => b.Description.Contains(keyword) || b.Title.Contains(keyword));

            results = results.OrderByDescending(b => b.AdId).Take(5);

            // ViewBag.Title = "Search results...";
            ViewBag.ResultsPerPage = ResultsPerPage;
            ViewBag.MaxPageRequests = MaxPageRequests;

            var searchModel = new SearchModel
            {
                SearchResults = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList()),
                SearchFilters = new SearchFilters
                {
                    Keyword = keyword
                }
            };

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
