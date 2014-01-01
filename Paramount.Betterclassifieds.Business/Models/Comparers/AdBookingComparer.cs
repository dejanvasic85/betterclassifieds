using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Betterclassifieds.Business.Models.Comparers
{
    public class AdBookingComparer: IEqualityComparer<AdBookingModel>
    {
        public bool Equals(AdBookingModel x, AdBookingModel y)
        {
            return x.AdBookingId == y.AdBookingId;
        }

        public int GetHashCode(AdBookingModel obj)
        {
            return obj.AdBookingId;
        }
    }
}
