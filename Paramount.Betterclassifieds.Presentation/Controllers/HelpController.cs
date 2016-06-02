using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HelpController : Controller
    {
        private readonly IClientConfig _clientConfig;

        public HelpController(IClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        // GET: Help
        [ActionName("how-it-works")]
        public ActionResult How()
        {
            return View();
        }

        // GET: Pricing
        [ActionName("event-pricing")]
        public ActionResult EventPricing()
        {
            var ticketCalculator = new TicketFeeCalculator(_clientConfig);
            var vm = new TicketingPricesExplainedViewModel
            {
                EventTicketFeePercentage = _clientConfig.EventTicketFeePercentage,
                EventTicketFeeCents = _clientConfig.EventTicketFeeCents,
            };

            vm.Example1Fee = ticketCalculator.GetTotalTicketPrice(vm.Example1TicketPrice).Fee;
            vm.Example2Fee = ticketCalculator.GetTotalTicketPrice(vm.Example2TicketPrice).Fee;
            vm.Example3Fee = ticketCalculator.GetTotalTicketPrice(vm.Example3TicketPrice).Fee;

            vm.ExampleTotalTicketSales = 100;
            vm.ExampleTotalTicketQuantitySold = 10;
            vm.ExampleTotalFeeForOrganiser = ticketCalculator.GetFeeTotalForOrganiserForAllTicketSales(100, 10);
            vm.ExampleTotalAmountForOrganiser = vm.ExampleTotalTicketSales - vm.ExampleTotalFeeForOrganiser;

            return View(vm);
        }

        [ActionName("barcode-validation")]
        public ActionResult BarcodeValidation()
        {
            return View();
        }

        [ActionName("realtime-data")]
        public ActionResult RealtimeDashboard()
        {
            return View();
        }
    }
}