namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventPaymentRequestViewModel
    {
        public int AdId { get; set; }
        public int EventId { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal TotalTicketSalesAmount { get; set; }
        public decimal OurFeesPercentage { get; set; }
        public string PreferredPaymentType { get; set; }
        public string PayPalEmail { get; set; }
        public DirectDebitViewModel DirectDebitDetails { get; set; }
    }
}