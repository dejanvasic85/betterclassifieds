namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPaymentSummary
    {
        public decimal TotalTicketSalesAmount { get; set; }
        public decimal SystemTicketFee { get; set; }
        public decimal EventOrganiserOwedAmount { get; set; }
    }
}