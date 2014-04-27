using AutoMapper;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HomeController : BaseController, IMappingBehaviour
    {
        public HomeController(ISearchService searchService)
            : base(searchService)
        { }

        public ActionResult Index()
        {
            var results = _searchService.GetLatestAds();

            return View(new HomeModel
            {
                AdSummaryList = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList())
            });
        }


        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
        }
    }
}
