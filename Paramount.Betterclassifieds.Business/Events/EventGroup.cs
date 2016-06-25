using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventGroup
    {
        public int? EventGroupId { get; set; }
        public int? EventId { get; set; }
        public string GroupName { get; set; }
        public int? MaxGuests { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public string CreatedBy { get; set; }
        public int GuestCount { get; set; }
    }
}