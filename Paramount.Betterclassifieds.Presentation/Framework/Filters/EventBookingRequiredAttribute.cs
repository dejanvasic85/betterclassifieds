﻿using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation
{
    public class EventBookingRequiredAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IEventBookingContext EventBookingContext { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var notFoundResult = new Redirector().NotFound();

            if (EventBookingContext?.EventBookingId == null)
            {
                filterContext.Result = notFoundResult;
            }
        }
    }
}