using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Authorizes the user to the ad they are requesting only if it belongs to them.
    /// </summary>
    public class AuthorizeBookingIdentity : ActionFilterAttribute
    {
        [Dependency]
        public ISearchService SearchService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object id = filterContext.ActionParameters["id"] ?? filterContext.RouteData.Values["id"];

            if (id == null)
            {
                throw new BookingAuthorisationException("id is not available in routeData");
            }

            int adId;
            if (!Int32.TryParse(id.ToString(), out adId))
            {
                throw new BookingAuthorisationException("id is not available in routeData");
            }

            var ad = SearchService.GetAdById(adId);
            var loggedInUser = filterContext.HttpContext.User.Identity.Name;
            if (ad == null || !ad.Username.Equals(loggedInUser))
            {
                throw new BookingAuthorisationException(string.Format("Ad Id {0} does not belong to {1}", adId, loggedInUser));
            }
        }
    }
}