namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventPaymentRequestViewModel
    {
        public int EventId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal RequestedAmount { get; set; }
    }
}