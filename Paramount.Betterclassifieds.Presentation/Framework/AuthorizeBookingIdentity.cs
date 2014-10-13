using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.Presentation
{
    /// <summary>
    /// Authorizes the user to the ad they are requesting only if it belongs to them.
    /// </summary>
    public class AuthorizeBookingIdentity : ActionFilterAttribute
    {
        [Dependency]
        public IBookingManager BookingManager { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            // todo   
        }
    }
}