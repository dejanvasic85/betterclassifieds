using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public class AdBookingIdComparer: IEqualityComparer<AdBookingModel>
    {
        public bool Equals(AdBookingModel x, AdBookingModel y)
        {
            return x.AdBookingId == y.AdBookingId;
        }

        public int GetHashCode(AdBookingModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
