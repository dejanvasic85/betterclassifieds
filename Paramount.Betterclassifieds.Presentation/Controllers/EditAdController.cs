using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeBookingIdentity]
    public class EditAdController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBookingManager _bookingManager;

        public EditAdController(ISearchService searchService, IApplicationConfig applicationConfig, IClientConfig clientConfig, IDocumentRepository documentRepository, IBookingManager bookingManager)
        {
            _searchService = searchService;
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
            _documentRepository = documentRepository;
            _bookingManager = bookingManager;
        }


        //
        // GET: /EditAd/AdDetails/{id}
        public ActionResult Details(int id)
        {
            ViewBag.Updated = false;
            // Fetch the ad booking
            var adBooking = _searchService.GetAdById(id);

            // Todo - use automapper
            var viewModel = new EditAdDetailsViewModel
            {
                Id = id,
                MaxOnlineImages = _clientConfig.MaxOnlineImages > adBooking.ImageUrls.Length ? _clientConfig.MaxOnlineImages : adBooking.ImageUrls.Length,
                MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes,
                ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount,
                StartDate = adBooking.StartDate,
                IsFutureScheduledAd = adBooking.StartDate > DateTime.Today,
                OnlineAdHeading = adBooking.Heading,
                OnlineAdDescription = adBooking.Description,
                OnlineAdContactEmail = adBooking.ContactEmail,
                OnlineAdContactName = adBooking.ContactName,
                OnlineAdContactPhone = adBooking.ContactPhone,
                OnlineAdLocationId = adBooking.LocationId,
                OnlineAdLocationAreaId = adBooking.LocationAreaId,
                OnlineAdImages = adBooking.ImageUrls.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Details(EditAdDetailsViewModel viewModel)
        {
            var adBooking = _searchService.GetAdById(viewModel.Id);
            viewModel.MaxOnlineImages = _clientConfig.MaxOnlineImages > adBooking.ImageUrls.Length ? _clientConfig.MaxOnlineImages : adBooking.ImageUrls.Length;
            viewModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;
            viewModel.ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount;
            viewModel.OnlineAdImages = adBooking.ImageUrls.ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.Updated = false;
                return View(viewModel);
            }

            // Convert to online ad
            OnlineAdModel onlineAd = this.Map<EditAdDetailsViewModel, OnlineAdModel>(viewModel);

            // Update the online ad
            _bookingManager.UpdateOnlineAd(viewModel.Id, onlineAd);

            // Map the settings
            ViewBag.Updated = true;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AssignOnlineImage(int id, string documentId)
        {
            _bookingManager.AddOnlineImage(id, documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveOnlineImage(int id, string documentId)
        {
            _bookingManager.RemoveOnlineImage(id, documentId.Trim());
            return Json(true);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.RecognizeDestinationPrefixes("OnlineAd", "Line");
            configuration.RecognizePrefixes("OnlineAd", "Line");

            configuration.CreateMap<EditAdDetailsViewModel, OnlineAdModel>()
               .ForMember(member => member.Images, options => options.Ignore());
        }
    }
}
