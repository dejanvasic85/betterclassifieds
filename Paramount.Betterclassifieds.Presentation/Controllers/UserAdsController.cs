﻿using System;
using System.Linq;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class UserAdsController : Controller
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

        public ActionResult GetAdsForUser()
        {
            var userAds = _bookingManager.GetBookingsForUser(User.Identity.Name);

            var viewModels = userAds.Select(ad => new UserBookingViewModel
            {
                AdId = ad.AdBookingId,
                AdImageId = ad.OnlineAd.DefaultImageId != null ? ad.OnlineAd.DefaultImageId.DocumentId : string.Empty,
                Heading = ad.OnlineAd.Heading,
                Description = ad.OnlineAd.Description.TruncateOnWordBoundary(200),
                Starts = ad.StartDate.ToString("dd-MMM-yyyy"),
                Ends = ad.EndDate.ToString("dd-MMM-yyyy"),
                Messages = 0,
                Status = GetViewStatusFrom(ad),
                Visits = ad.OnlineAd.NumOfViews
            });

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        private string GetViewStatusFrom(AdBookingModel ad)
        {
            if (ad.IsExpired)
                return "Expired";

            if (ad.IsFutureAd)
                return "Scheduled";

            if (ad.IsCurrentAd)
                return "Current";

            throw new ArgumentException("Unable to retrieve view status from booking");
        }

        [HttpPost]
        public ActionResult Cancel(int adId)
        {
            // Todo - wire the backend

            return Json(true);
        }
    }
}