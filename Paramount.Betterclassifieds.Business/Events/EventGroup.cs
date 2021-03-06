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
        public bool? AvailableToAllTickets { get; set; }
        public bool IsDisabled { get; set; }

        public bool IsFull()
        {
            if (!MaxGuests.HasValue)
                return false;
            return GuestCount >= MaxGuests.Value;
        }

        public bool IsAvailable()
        {
            return !IsFull() && !IsDisabled;
        }
    }
}