using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public struct Discount
    {
        public decimal OriginalAmount { get; }
        public decimal DiscountPercent { get; }
        public decimal DiscountValue { get; set; }
        public decimal AmountAfterDiscount { get; set; }

        public Discount(decimal amount, decimal discountPercent)
        {
            OriginalAmount = amount;
            DiscountPercent = discountPercent;

            if (discountPercent < 0 || discountPercent > 100)
            {
                throw new ArgumentOutOfRangeException("discountPercent", "must be between 0 and 100");
            }

            var discount = discountPercent / 100;

            DiscountValue = discount * amount;
            AmountAfterDiscount = amount - DiscountValue;
        }

        public static implicit operator decimal(Discount d)
        {
            return d.AmountAfterDiscount;
        }

        public static Discount MinValue => new Discount(0, 0);
    }
}