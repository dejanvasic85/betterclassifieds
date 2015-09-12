﻿using System.Linq;
using System.Monads;
using System.Web.Mvc;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EventAdController : Controller, IMappingBehaviour
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISearchService _searchService;

        public EventAdController(IEventRepository eventRepository, ISearchService searchService)
        {
            _eventRepository = eventRepository;
            _searchService = searchService;
        }

        //
        // GET: /EventDetail/

        public ActionResult Index(int id, string titleSlug = "")
        {
            var onlineAdModel = _searchService.GetAdById(id);

            if (onlineAdModel == null)
            {
                return View("~/Views/Listings/404.cshtml");
            }

            var eventModel = _eventRepository.GetEventDetails(onlineAdModel.OnlineAdId);
            var eventViewModel = this.Map<Business.Events.EventModel, EventViewModel>(eventModel);

            eventViewModel.Title = onlineAdModel.Heading;
            eventViewModel.Description = onlineAdModel.Description;
            eventViewModel.OrganiserName = onlineAdModel.ContactName;
            eventViewModel.OrganiserPhone = onlineAdModel.ContactPhone;
            eventViewModel.EventPhoto = onlineAdModel.ImageUrls.With(i => i.FirstOrDefault());
            eventViewModel.Views = onlineAdModel.NumOfViews;
            eventViewModel.Posted = onlineAdModel.BookingDate.HasValue ? onlineAdModel.BookingDate.Value.Humanize(utcDate: false) : string.Empty;

            return View(eventViewModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Business.Events.EventModel, EventViewModel>()
                .ForMember(member => member.EventStartDate, options => options.ResolveUsing(src => src.EventStartDate.GetValueOrDefault().ToLongDateString()))
                .ForMember(member => member.EventStartTime, options => options.ResolveUsing(src => src.EventStartDate.GetValueOrDefault().ToString("hh:mm tt")))
                .ForMember(member => member.EventEndDate, options => options.ResolveUsing(src => src.EventEndDate.GetValueOrDefault().ToLongDateString()))
                ;
            configuration.CreateMap<Business.Events.EventTicket, EventTicketViewModel>();
        }
    }
}
