﻿using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Ensures access only if the booking is in progress otherwise redirects to the first step
    /// </summary>
    public class BookingRequiredAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IBookingSessionIdentifier CurrentBookingId { get; set; }

        [Dependency]
        public IBookingCartRepository Repository { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentBookingId.Id.IsNullOrEmpty() || Repository.GetBookingCart(CurrentBookingId.Id) == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "booking"},
                    {"action", "Step1"}
                });
            }
        }
    }
}