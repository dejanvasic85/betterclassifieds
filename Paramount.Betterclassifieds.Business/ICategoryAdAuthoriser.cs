using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public interface ICategoryAdAuthoriser
    {
        bool IsUserAuthorisedForAdId(string username, AdBookingModel adBookingModel);
        IEnumerable<int> GetAdsForUser(string username);
    }
}