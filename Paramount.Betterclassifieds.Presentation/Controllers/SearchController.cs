using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class SearchController : Controller, IMappingBehaviour
    {
        private readonly IBookingManager _manager;

        public SearchController(IBookingManager manager)
        {
            _manager = manager;
        }

        //
        // GET: /Search/
        public ActionResult Index(string keyword = "")
        {
            var bookings = _manager.GetLatestBookings(1000);
            
            var results = this.MapList<AdBookingModel,AdSummaryViewModel>(bookings).AsEnumerable();

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
