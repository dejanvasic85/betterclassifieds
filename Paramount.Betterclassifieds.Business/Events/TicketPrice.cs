using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class TicketPrice
    {
        public TicketPrice()
        {
            // All the decimals should obtain the zero default values
        }

        public TicketPrice(decimal originalPrice, decimal priceIncludingFee, decimal fee)
            : this(originalPrice, priceIncludingFee, fee, decimal.MinValue, decimal.MinValue)
        {
        }

        public TicketPrice(decimal originalPrice, decimal priceIncludingFee, decimal fee, decimal discountPercent, decimal discountAmount)
        {
            OriginalPrice = originalPrice;
            PriceIncludingFee = priceIncludingFee;
            Fee = fee;
            DiscountPercent = discountPercent;
            DiscountAmount = discountAmount;
        }

        public decimal OriginalPrice { get; }
        public decimal PriceIncludingFee { get; }
        public decimal Fee { get; }
        public decimal DiscountPercent { get; }
        public decimal DiscountAmount { get; }
    }
}