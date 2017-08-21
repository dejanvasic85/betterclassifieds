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
            return View();
        }
        
        // 
        // POST: /Cancel/Id 
        // JSON
        [HttpPost]
        public ActionResult Cancel(int adId)
        {
            _bookingManager.CancelAd(adId);

            return Json(true);
        }
    }
}
