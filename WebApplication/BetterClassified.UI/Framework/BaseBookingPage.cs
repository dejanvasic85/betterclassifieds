using System.Web.Security;
using BetterclassifiedsCore;
using BetterclassifiedsCore.BundleBooking;
using BetterclassifiedsCore.BusinessEntities;
using System;
using System.Web;

namespace BetterClassified.UI.WebPage
{
    /// <summary>
    /// Base page for webforms
    /// </summary>
    public class BaseBookingPage : System.Web.UI.Page
    {
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);

            var context = HttpContext.Current;
            var exception = context.Server.GetLastError();
            
            if (exception is BookingExpiredException)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);

                // Clear the last error and do a redirect to the first booking step
                context.Server.ClearError();
                context.Response.Redirect("~/Booking/Step1.aspx?action=expired");
            }
        }
    }

    public class BaseBundlePage : BaseBookingPage
    {
        protected override void OnLoad(EventArgs e)
        {
            var user = Membership.GetUser();
            if (user == null || !user.IsOnline)
            {
                // Redirect to the log in page
                Response.Redirect("~/Account/Login");
            }

            if(BundleController.BundleCart == null)
                throw new BookingExpiredException("BundleController.BundleCart is null");

            if (AdController.TempRecordExist(BundleController.BundleCart.BookReference))
                throw new BookingExpiredException("BundleController.BundleCart.BookReference already exists in TempBookingRecord");

            base.OnLoad(e);
        }
    }

    public class BaseOnlineBookingPage : BaseBookingPage
    {
        protected override void OnLoad(EventArgs e)
        {
            if (BookingController.AdBookCart == null)
                throw new BookingExpiredException("BookingController.AdBookCart is null");

            if (AdController.TempRecordExist(BookingController.AdBookCart.BookReference))
                throw new BookingExpiredException("BookingController.AdBookCart.BookReference already exists in TempBookingRecord");

            base.OnLoad(e);
        }
    }
}
