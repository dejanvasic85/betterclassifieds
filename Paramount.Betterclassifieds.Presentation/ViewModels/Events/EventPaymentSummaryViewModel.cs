namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventPaymentSummaryViewModel
    {
        public int AdId { get; set; }
        public int EventId { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal TotalTicketSalesAmount { get; set; }
        public decimal OurFeesPercentage { get; set; }
        public string PreferredPaymentType { get; set; }
        public string PayPalEmail { get; set; }
        public DirectDebitViewModel DirectDebitDetails { get; set; }

        public bool HasDirectDebitDetails 
        {
            get
            {
                if (this.DirectDebitDetails == null)
                    return false;

                return DirectDebitDetails.IsConfigured();
            }
        }
    }
}