using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class EditAdController : Controller
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
        [AuthorizeBookingIdentity]
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
        [AuthorizeBookingIdentity]
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

            // Map the settings
            ViewBag.Updated = true;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadOnlineImage()
        {
            var files = Request.Files.Cast<string>()
               .Select(file => Request.Files[file].As<HttpPostedFileBase>())
               .Where(postedFile => postedFile != null && postedFile.ContentLength != 0)
               .ToList();

            // There should only be 1 uploaded file so just check the size ...
            var uploadedFile = files.Single();
            if (uploadedFile.ContentLength > _applicationConfig.MaxImageUploadBytes)
            {
                return Json(new { errorMsg = "The file exceeds the maximum file size." });
            }

            if (!_applicationConfig.AcceptedImageFileTypes.Any(type => type.Equals(uploadedFile.ContentType)))
            {
                return Json(new { errorMsg = "Not an accepted file type." });
            }

            var documentId = Guid.NewGuid();

            var imageDocument = new Document(documentId, uploadedFile.InputStream.FromStream(), uploadedFile.ContentType,
                uploadedFile.FileName, uploadedFile.ContentLength, this.User.Identity.Name);

            _documentRepository.Save(imageDocument);

            return Json(new { documentId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AssignOnlineImage(int adId, string documentId)
        {
            _bookingManager.AddOnlineImage(adId, documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveOnlineImage(int adId, string documentId)
        {
            _bookingManager.RemoveOnlineImage(adId, documentId.Trim());
            return Json(true);
        }
    }
}
