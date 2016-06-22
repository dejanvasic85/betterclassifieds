namespace Paramount.Betterclassifieds.Business.Events
{
    public class TicketPrice
    {
        public TicketPrice()
        {
            // All the decimals should obtain the zero default values
        }

        public TicketPrice(decimal originalPrice, decimal priceIncludingFee, decimal fee)
        {
            OriginalPrice = originalPrice;
            PriceIncludingFee = priceIncludingFee;
            Fee = fee;
        }

        public decimal OriginalPrice { get; private set; }
        public decimal PriceIncludingFee { get; private set; }
        public decimal Fee { get; private set; }
    }
}