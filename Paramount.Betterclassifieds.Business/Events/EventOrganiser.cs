using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventOrganiser
    {
        public int EventOrganiserId { get; set; }
        public int EventId { get; set; }
        public EventModel Event { get; set; }
        public string UserId { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
