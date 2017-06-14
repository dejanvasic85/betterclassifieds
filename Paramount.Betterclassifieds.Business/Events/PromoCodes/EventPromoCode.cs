using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPromoCode
    {
        public long EventPromoCodeId { get; set; }
        public int EventId { get; set; }
        public EventModel Event { get; set; }
        public string PromoCode { get; set; }
        public decimal? DiscountPercent { get; set; }
        public bool? IsDisabled { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }

        /// <summary>
        /// Returns the new value to be used for amount after applying the discount
        /// </summary>
        public Discount ApplyDiscountFor(decimal amount)
        {
            if (IsDisabled.GetValueOrDefault())
            {
                throw new OperationOnDisabledValueException("Cannot apply discount for a disabled Promo Code");
            }
            
            return new Discount(amount, this.DiscountPercent.GetValueOrDefault(0));
        }

        public override string ToString()
        {
            return PromoCode;
        }
    }
}