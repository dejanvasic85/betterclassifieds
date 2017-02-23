using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business
{
    public interface ICategoryAdAuthoriser
    {
        bool IsUserAuthorisedForAd(string username, AdBookingModel adBookingModel);
        IEnumerable<int> GetOnlineAdsForUser(string username);
    }
}