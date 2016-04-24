using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class MakePaymentViewModel
    {
        public decimal TotalCost { get; set; }
        public int TotalCostCents => (int)(TotalCost * 100);
        public List<EventBookingTicketViewModel> EventTickets { get; set; }
        public string StripePublishableKey { get; set; }
    }
}