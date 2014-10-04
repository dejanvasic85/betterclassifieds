﻿using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BookingRequiredAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IBookingId CurrentBookingId { get; set; }

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