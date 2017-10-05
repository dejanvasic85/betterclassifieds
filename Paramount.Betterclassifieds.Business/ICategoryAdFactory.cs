using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface ICategoryAdFactory
    {
        ICategoryAdRepository<ICategoryAd> CreateRepository(Booking.IBookingCart bookingCart);
        ICategoryAdAuthoriser CreateAuthoriser(string name);
        IEnumerable<ICategoryAdAuthoriser> CreateAuthorisers();
        ICategoryAdUrlService CreateUrlService(string categoryAdType);
    }
}