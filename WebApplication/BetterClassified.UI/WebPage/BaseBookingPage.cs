using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BetterclassifiedsCore.BusinessEntities;
using Paramount.Modules.Logging.UIController;
using BetterclassifiedsCore;

namespace BetterClassified.UI.WebPage
{
    public class BaseBookingPage : System.Web.UI.Page
    {
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);

            var context = HttpContext.Current;
            var exception = context.Server.GetLastError();

            // Log / Audit using Paramount Service
            ExceptionLogController<Exception>.AuditException(exception);
            context.Server.ClearError();

            if (exception is BookingExpiredException)
            {
                // Redirect to Page
                context.Response.Redirect(WebPageUrl.BookingExpired);
            }
        }
    }
}
