﻿using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeBookingIdentity]
    public class EditAdController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;
        private readonly IBookingManager _bookingManager;
        private readonly IEventManager _eventManager;

        public EditAdController(ISearchService searchService, IApplicationConfig applicationConfig, IClientConfig clientConfig, IBookingManager bookingManager, IEventManager eventManager)
        {
            _searchService = searchService;
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
            _bookingManager = bookingManager;
            _eventManager = eventManager;
        }

        //
        // GET: /EditAd/AdDetails/{id}
        public ActionResult Details(int id)
        {
            ViewBag.Updated = false;
            ViewBag.Invalid = false;


            // Fetch the ad booking
            var adBooking = _bookingManager.GetBooking(id);
            var onlineAd = adBooking.OnlineAd;

            var viewModel = new EditAdDetailsViewModel
            {
                Id = id,
                MaxOnlineImages = _clientConfig.MaxOnlineImages > onlineAd.Images.Count ? _clientConfig.MaxOnlineImages : onlineAd.Images.Count,
                MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes,
                ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount,
                StartDate = adBooking.StartDate,
                IsFutureScheduledAd = adBooking.StartDate >= DateTime.Today,
                OnlineAdImages = onlineAd.Images.Select(a => a.DocumentId).ToList(),
            };

            // Online ad mapping
            this.Map(adBooking.OnlineAd, viewModel);

            if (!adBooking.HasLineAd)
            {
                return View(viewModel);
            }

            // Line ad mapping
            viewModel.IsLineAdIncluded = true;
            viewModel.LineWordsPurchased = adBooking.LineAd.NumOfWords;
            this.Map(adBooking.LineAd, viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(EditAdDetailsViewModel viewModel)
        {
            var adBooking = _searchService.GetByAdId(viewModel.Id);
            viewModel.MaxOnlineImages = _clientConfig.MaxOnlineImages > adBooking.ImageUrls.Length ? _clientConfig.MaxOnlineImages : adBooking.ImageUrls.Length;
            viewModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;
            viewModel.ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount;
            viewModel.OnlineAdImages = adBooking.ImageUrls.ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.Updated = false;
                ViewBag.Invalid = true;
                return View(viewModel);
            }

            // Convert to online ad
            var onlineAd = this.Map<EditAdDetailsViewModel, OnlineAdModel>(viewModel);
            onlineAd.SetDescription(viewModel.OnlineAdDescription);

            // Update the online ad
            _bookingManager.UpdateOnlineAd(viewModel.Id, onlineAd);

            if (viewModel.IsLineAdIncluded)
            {
                // Update the line ad
                var lineAd = this.Map<EditAdDetailsViewModel, LineAdModel>(viewModel);
                _bookingManager.UpdateLineAd(viewModel.Id, lineAd);
            }

            // Set the schedule
            if (viewModel.StartDate.HasValue && adBooking.StartDate != viewModel.StartDate)
            {
                _bookingManager.UpdateSchedule(viewModel.Id, viewModel.StartDate.GetValueOrDefault());
            }

            ViewBag.Updated = true;
            ViewBag.Invalid = false;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AssignOnlineImage(int id, Guid documentId)
        {
            _bookingManager.AddOnlineImage(id, documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveOnlineImage(int id, Guid documentId)
        {
            _bookingManager.RemoveOnlineImage(id, documentId);
            return Json(true);
        }

        [HttpPost, BookingRequired]
        public ActionResult AssignLineAdImage(int id, Guid documentId)
        {
            _bookingManager.AssignLineAdImage(id, documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveLineAdImage(int id, Guid documentId)
        {
            _bookingManager.RemoveLineAdImage(id, documentId);
            return Json(true);
        }

        public ActionResult Event(int id)
        {
            var adDetails = _searchService.GetByAdId(id);
            var eventDetails = _eventManager.GetEventDetailsForOnlineAdId(adDetails.OnlineAdId);
            var evenTicketTypes = eventDetails.Tickets;
            var eventEditViewModel = new EventEditViewModel
            {
                AdId = id,
                Tickets = this.MapList<EventTicket, EventTicketViewModel>(evenTicketTypes.ToList())
            };
            return View(eventEditViewModel);
        }

        [HttpPost]
        public ActionResult EventTicketUpdate(int id, EventTicketViewModel eventTicketViewModel)
        {
            _eventManager.UpdateEventTicket(eventTicketViewModel.EventTicketId.GetValueOrDefault(),
                eventTicketViewModel.TicketName,
                eventTicketViewModel.Price,
                eventTicketViewModel.RemainingQuantity);

            return Json(new { Updated = true });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.RecognizeDestinationPrefixes("OnlineAd", "Line");
            configuration.RecognizePrefixes("OnlineAd", "Line");

            // To view model
            configuration.CreateMap<OnlineAdModel, EditAdDetailsViewModel>()
                .ForMember(m => m.OnlineAdImages, options => options.Ignore())
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.OnlineAdDescription, options => options.MapFrom(src => src.HtmlText));

            configuration.CreateMap<LineAdModel, EditAdDetailsViewModel>()
                .ForMember(m => m.Id, options => options.Ignore());

            // From view model
            configuration.CreateMap<EditAdDetailsViewModel, OnlineAdModel>()
               .ForMember(member => member.Images, options => options.Ignore())
               .ForMember(member => member.HtmlText, options => options.MapFrom(src => src.OnlineAdDescription));

            configuration.CreateMap<EditAdDetailsViewModel, LineAdModel>()
                .ForMember(member => member.UsePhoto, options => options.MapFrom(src => src.LineAdImageId.HasValue()));

            configuration.CreateMap<EventTicket, EventTicketViewModel>();
        }
    }
}
