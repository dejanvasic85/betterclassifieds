﻿using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HomeController : BaseController, IMappingBehaviour
    {
        private readonly IBookingManager _bookingManager;

        public HomeController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

#if RELEASE
        [OutputCache(Duration = 120)]
#endif
        public ActionResult Index()
        {
            var ads = this.MapList<AdBookingModel, AdSummaryViewModel>(_bookingManager.GetLatestBookings());
            
            return View(new HomeModel
            {
                AdSummaryList = ads
            });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From Business
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