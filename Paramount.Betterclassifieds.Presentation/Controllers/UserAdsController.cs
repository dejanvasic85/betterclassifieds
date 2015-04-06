using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class UserAdsController : Controller
    {
        //
        // GET: /UserAd/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAdsForUser()
        {
            // Todo - Wire up the real backend

            var viewModel = new List<UserBookingViewModel>
            {
                new UserBookingViewModel { Status = "Current" ,AdId = 14458, AdImageId = "6ba06205-5b1d-4f9f-b7c1-543665c17775", Ends = "20th May", Heading = "Title of the Ad", Messages = 2, Starts = "2 days ago", Visits = 20, Description = "Code School teaches web technologies in the comfort of your browser with video lessons, coding challenges, and screencasts."},
                new UserBookingViewModel { Status = "Future" ,AdId = 14459, AdImageId = "6ba06205-5b1d-4f9f-b7c1-543665c17775", Ends = "20th May", Heading = "Title of the Ad", Messages = 2, Starts = "2 days ago", Visits = 20, Description = "Code School teaches web technologies in the comfort of your browser with video lessons, coding challenges, and screencasts."},
                new UserBookingViewModel { Status = "Expired" ,AdId = 14490, AdImageId = "6ba06205-5b1d-4f9f-b7c1-543665c17775", Ends = "20th May", Heading = "Title of the Ad", Messages = 2, Starts = "2 days ago", Visits = 20, Description = "Code School teaches web technologies in the comfort of your browser with video lessons, coding challenges, and screencasts."},
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cancel(int adId)
        {
            // Todo - wire the backend

            return Json(true);
        }
    }
}
