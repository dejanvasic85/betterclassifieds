namespace Paramount.Betterclassifieds.Business.Events
{
    public class TicketPrice
    {
        public decimal OriginalPrice { get; set; }
        public decimal PriceIncludingFee { get; set; }
        public decimal Fee { get; set; }
    }
}