using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventGuestService
    {
        IEnumerable<EventGuestDetails> BuildGuestList(int? eventId);
        IEnumerable<EventGuestPublicView> GetPublicGuestNames(int? eventId);
        IEnumerable<EventGuestPublicView> GetFirstNames(IEnumerable<EventGuestDetails> guests);
    }

}