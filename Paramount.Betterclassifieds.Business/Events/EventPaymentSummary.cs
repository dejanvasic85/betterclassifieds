namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPaymentSummary
    {
        public decimal TotalTicketSalesAmount { get; set; }
        public decimal SystemTicketFee { get; set; }
        /// <summary>
        /// Total amount that should go to the event organiser
        /// </summary>
        public decimal EventOrganiserOwedAmount { get; set; }
    }
}