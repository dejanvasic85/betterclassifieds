using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPromotionGuest
    {
        public long? EventPromotionGuestId { get; set; }
        public int UserNetworkId { get; set; }
        public int EventId { get; set; }
        public EventModel EventModel { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
    }
}