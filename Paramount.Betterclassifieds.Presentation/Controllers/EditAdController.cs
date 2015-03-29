using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeCartIdentity]
    public class EditAdController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;

        public EditAdController(ISearchService searchService, IApplicationConfig applicationConfig,
            IClientConfig clientConfig)
        {
            _searchService = searchService;
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
        }


        //
        // GET: /EditAd/AdDetails/{id}
        
        public ActionResult Details(int id)
        {
            ViewBag.Updated = false;
            var viewModel = new EditAdDetailsViewModel();

            // Fetch the ad booking
            var adBooking = _searchService.GetAdById(id);

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
    }
}
