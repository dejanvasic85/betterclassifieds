using System;

namespace Paramount.Betterclassifieds.Business
{
    [Obsolete("We don't really need any of these values except for booked. Need to refactor this area.")]
    public enum BookingStatusType
    {
        Booked = 1,
        Expired = 2,
        Cancelled = 3,
        Unpaid = 4,
    }
}