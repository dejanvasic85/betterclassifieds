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
        
        public override string ToString()
        {
            return PromoCode;
        }
    }

    public class EventPromoCodeFactory
    {
        public static EventPromoCode Create(int eventId, string promoCode, decimal? discountPercent)
        {
            return new EventPromoCode
            {
                PromoCode = promoCode,
                DiscountPercent = discountPercent,
                CreatedDate = DateTime.Now,
                CreatedDateUtc = DateTime.UtcNow,
                EventId = eventId,
                IsDisabled = false,
            };
        }
    }
}