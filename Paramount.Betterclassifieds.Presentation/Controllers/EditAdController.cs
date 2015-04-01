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
        [AuthorizeCartIdentity]
        public ActionResult Details(int id)
        {
            ViewBag.Updated = false;
            var viewModel = new EditAdDetailsViewModel();

            // Fetch the ad booking
            var adBooking = _searchService.GetAdById(id);

            // Todo - use automapper
            viewModel.MaxOnlineImages = adBooking.ImageUrls.Length;
            viewModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;
            viewModel.ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount; // maximum duration days for a booking
            viewModel.StartDate = adBooking.StartDate;
          
            viewModel.OnlineAdHeading = adBooking.Heading;
            viewModel.OnlineAdDescription = adBooking.Description;
            viewModel.OnlineAdContactEmail = adBooking.ContactEmail;
            viewModel.OnlineAdContactName = adBooking.ContactName;
            viewModel.OnlineAdContactPhone = adBooking.ContactPhone;
            viewModel.OnlineAdLocationId = adBooking.LocationId;
            viewModel.OnlineAdLocationAreaId = adBooking.LocationAreaId;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(EditAdDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Updated = false;
            }

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
        public ActionResult AssignOnlineImage(int adId)
        {
            return Json(true);
        }
    }
}
