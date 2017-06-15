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
}