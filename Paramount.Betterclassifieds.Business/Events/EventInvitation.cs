using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventInvitation
    {
        public long? EventInvitationId { get; set; }
        public int UserNetworkId { get; set; }
        public int EventId { get; set; }
        public EventModel EventModel { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ConfirmedDateUtc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
    }
}