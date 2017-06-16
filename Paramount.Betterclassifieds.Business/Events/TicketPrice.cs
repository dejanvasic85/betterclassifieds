using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class TicketPrice
    {
        private TicketPrice() { }

        public TicketPrice(decimal originalPrice, decimal total, decimal fee)
            : this(originalPrice, total, fee, decimal.MinValue, decimal.MinValue, originalPrice)
        {
        }

        public TicketPrice(decimal originalPrice, decimal total, decimal fee,
            decimal discountPercent, decimal discountAmount, decimal priceAfterDiscount)
        {
            OriginalPrice = originalPrice;
            Total = total;
            Fee = fee;
            DiscountPercent = discountPercent;
            DiscountAmount = discountAmount;
            PriceAfterDiscount = priceAfterDiscount;
        }

        public decimal OriginalPrice { get; }
        public decimal Total { get; }
        public decimal Fee { get; }
        public decimal DiscountPercent { get; }
        public decimal DiscountAmount { get; }
        public decimal PriceAfterDiscount { get; }

        public static TicketPrice MinValue => new TicketPrice();
    }
}