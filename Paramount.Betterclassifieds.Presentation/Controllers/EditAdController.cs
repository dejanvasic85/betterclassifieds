using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Print;
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
            var adBooking = _bookingManager.GetBooking(id);
            var onlineAd = adBooking.OnlineAd;

            var viewModel = new EditAdDetailsViewModel
            {
                Id = id,
                MaxOnlineImages = _clientConfig.MaxOnlineImages > onlineAd.Images.Count ? _clientConfig.MaxOnlineImages : onlineAd.Images.Count,
                MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes,
                ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount,
                StartDate = adBooking.StartDate,
                IsFutureScheduledAd = adBooking.StartDate > DateTime.Today,
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
            this.Map(adBooking.LineAd, viewModel);

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
            var onlineAd = this.Map<EditAdDetailsViewModel, OnlineAdModel>(viewModel);

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

            // To view model
            configuration.CreateMap<OnlineAdModel, EditAdDetailsViewModel>()
                .ForMember(m => m.OnlineAdImages, options => options.Ignore())
                .ForMember(m => m.Id, options => options.Ignore());

            configuration.CreateMap<LineAdModel, EditAdDetailsViewModel>()
                .ForMember(m => m.Id, options => options.Ignore());

            // From view model
            configuration.CreateMap<EditAdDetailsViewModel, OnlineAdModel>()
               .ForMember(member => member.Images, options => options.Ignore());
        }
    }
}
