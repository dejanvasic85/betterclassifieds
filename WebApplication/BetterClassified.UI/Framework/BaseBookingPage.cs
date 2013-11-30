using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BetterclassifiedsCore.BusinessEntities;
using BetterclassifiedsCore;

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
                // Clear the last error and do a redirect to the first booking step
                context.Server.ClearError();
                context.Response.Redirect("~/Booking/Step1.aspx?action=expired");
            }
        }
    }
}
