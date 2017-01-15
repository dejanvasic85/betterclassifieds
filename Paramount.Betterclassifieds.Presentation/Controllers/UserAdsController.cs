using System;
using System.Linq;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class UserAdsController : ApplicationController
    {
        private readonly IBookingManager _bookingManager;

        public UserAdsController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        //
        // GET: /UserAd/

        public ActionResult Index()
        {
            var userAds = _bookingManager.GetBookingsForUser(User.Identity.Name, takeMax: 20);
            var viewModels = userAds.Select(ad => new UserBookingViewModel(ad, Url)).ToList();
            return View(viewModels);
        }

        public ActionResult GetAdsForUser()
        {
            var userAds = _bookingManager.GetBookingsForUser(User.Identity.Name, takeMax: 20);
            var viewModels = userAds.Select(ad => new UserBookingViewModel(ad, Url));
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cancel(int adId)
        {
            _bookingManager.CancelAd(adId);

            return Json(true);
        }
    }
}
