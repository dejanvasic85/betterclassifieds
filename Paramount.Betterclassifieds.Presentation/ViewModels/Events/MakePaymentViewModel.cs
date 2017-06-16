using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class MakePaymentViewModel
    {
        public decimal TotalCost { get; set; }
        public int TotalCostCents => (int)(TotalCost * 100);
        public List<EventBookingTicketViewModel> EventTickets { get; set; }
        public string StripePublishableKey { get; set; }
        public bool EnablePayPalPayments { get; set; }
        public bool EnableCreditCardPayments { get; set; }
        public string PaymentFailedMessage { get; set; }
        public decimal TotalCostIncludingDiscount { get; set; }
        public decimal TotalFees { get; set; }
        public bool PromoDiscountApplied { get; set; }
        public decimal? PromoDiscountAmount { get; set; }
    }
}