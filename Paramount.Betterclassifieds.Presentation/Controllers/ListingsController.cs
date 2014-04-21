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

        public ListingsController( ISearchService searchService )
        {
            _searchService = searchService;
        }

        public ActionResult Find(string keyword = "", int? categoryId = null, int? locationId = null )
        {
            // We should pass the filters to the search 
            // But this is just to get the front end cshtml files functioning
            var results =  _searchService.Search();

            if (keyword.HasValue())
                results = results.Where(b => b.Description.Contains(keyword) || b.Title.Contains(keyword));

            results = results.OrderByDescending(b => b.AdId).Take(5);

            ViewBag.Title = "Search results...";
            
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

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("ListingsCtrlProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
        }
    }
}
