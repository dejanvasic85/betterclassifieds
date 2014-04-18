using System.Linq;
using AutoMapper;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HomeController : BaseController, IMappingBehaviour
    {
        private readonly ISearchService _searchService;

        public HomeController( ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult Index()
        {
            var results = _searchService.Search().OrderByDescending(b => b.AdId).Take(5);
            
            return View(new HomeModel
            {
                AdSummaryList = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList())
            });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();

            //// From Business
            //configuration.CreateMap<AdBookingModel, AdSummaryViewModel>()
            //    .ForMember(member => member.AdId, options => options.MapFrom(source => source.AdBookingId))
            //    .ForMember(member => member.Description,
            //        options => options.MapFrom(source => source.OnlineAd.Description))
            //    .ForMember(member => member.Title, options => options.MapFrom(source => source.OnlineAd.Heading))
            //    .ForMember(member => member.CategoryName, options => options.MapFrom(source => source.Category.Title))
            //    .ForMember(member => member.Publications,
            //        options => options.MapFrom(source => source.Publications.Select(p => p.Title)))
            //    .ForMember(member => member.ImageUrls,
            //        options => options.MapFrom(source => source.OnlineAd.Images.Select(i => i.ImageUrl)));

        }
    }
}
