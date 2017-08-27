using Paramount.Betterclassifieds.Business.Booking;
using System.Web.Mvc;
using System.Web.UI;
using Paramount.Betterclassifieds.Presentation.Services;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class UserAdsController : ApplicationController
    {
        private readonly IBookingManager _bookingManager;
        private readonly IUrl _url;

        public UserAdsController(IBookingManager bookingManager, IUrl url)
        {
            _bookingManager = bookingManager;
            _url = url;
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

            return Json(new
            {
                NextUrl = _url.UserAds()
            });
        }
    }
}
