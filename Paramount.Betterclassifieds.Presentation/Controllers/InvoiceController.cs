using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
//        [AuthorizeBookingIdentity]
        public ActionResult Booking(string id)
        {
            // Generates the invoice based on the order items for the booking Id
            int bookingId;
            int.TryParse(id, out bookingId);

            _invoiceService.GenerateBookingInvoice(bookingId);

            return View();
        }
    }
}