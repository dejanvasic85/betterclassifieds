namespace Paramount.Betterclassifieds.Presentation.ViewModels.Email
{
    public class EventPaymentRequestEmail
    {
        public string EventName { get; set; }
        public int EventId { get; set; }
        public string PreferredPaymentMethod { get; set; }
        public decimal RequestedAmount { get; set; }
        public string RequestEmail { get; set; }
        public string RequestUsername { get; set; }
    }
}