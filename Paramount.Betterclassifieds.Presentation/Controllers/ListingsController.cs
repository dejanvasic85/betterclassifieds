using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ListingsController : Controller, IMappingBehaviour
    {
        private readonly IBookingManager _manager;


        public ListingsController( IBookingManager manager)
        {
            _manager = manager;
        }

        public ActionResult Find(string keyword = "")
        {
            var bookings = _manager.GetLatestBookings(1000);
            var results = this.MapList<AdBookingModel, AdSummaryViewModel>(bookings).AsEnumerable();

            if (keyword.HasValue())
                results = results.Where(b => b.Description.Contains(keyword) || b.Title.Contains(keyword));

            ViewBag.Title = "Search results";

            var searchModel = new SearchModel
            {
                SearchResults = results.ToList(),
                SearchFilters = new SearchFilters
                {
                    Keyword = keyword
                }
            };

            return View(searchModel);
        }

        //public ActionResult Index(string seoName, int index = 0, int pageSize = 10)
        //{
        //    var onlineAds = new List<ListingSummaryViewModel>();
        //    if (string.IsNullOrEmpty(seoName))
        //    {
        //        return View(onlineAds);
        //    }

        //    var seoModel = GetMappingModel(seoName);

        //    if (seoModel == null)
        //    {
        //        return View(onlineAds);
        //    }

        //    onlineAds = GetListingBySeo(seoModel, index, pageSize).Select(a => new ListingSummaryViewModel(a)).ToList();

        //    return View(onlineAds);
        //}

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdBookingModel, AdSummaryViewModel>()
                .ForMember(member => member.AdId, options => options.MapFrom(source => source.AdBookingId))
                .ForMember(member => member.Description,
                    options => options.MapFrom(source => source.OnlineAd.Description))
                .ForMember(member => member.Title, options => options.MapFrom(source => source.OnlineAd.Heading))
                .ForMember(member => member.CategoryName, options => options.MapFrom(source => source.Category.Title))
                .ForMember(member => member.Publications,
                    options => options.MapFrom(source => source.Publications.Select(p => p.Title)))
                .ForMember(member => member.ImageUrls,
                    options => options.MapFrom(source => source.OnlineAd.Images.Select(i => i.ImageUrl)));
        }
    }
}
