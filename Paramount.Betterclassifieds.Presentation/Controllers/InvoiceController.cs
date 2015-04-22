using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        public InvoiceController()
        {
            
        }

        [HttpGet]
        [AuthorizeBookingIdentity]
        public ActionResult Index(string id)
        {
            // Prints out the invoice to a new page

            return View();
        }
    }
}