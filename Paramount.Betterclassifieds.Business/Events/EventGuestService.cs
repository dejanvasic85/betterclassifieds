using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventGuestService : IEventGuestService
    {
        private readonly IEventRepository _eventRepository;

        public EventGuestService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<EventGuestDetails> BuildGuestList(int? eventId)
        {
            Guard.NotNull(eventId);

            var tickets = _eventRepository.GetEventBookingTicketsForEvent(eventId);

            // Need to fetch all the groups to match each guest to the group
            // We cannot do that at the moment because groups cannot be fetched from EntityFramework (separate stored procedure)
            var groups = _eventRepository.GetEventGroups(eventId.GetValueOrDefault(), eventTicketId: null);

            return tickets.Select(t => new EventGuestDetails
            {
                GuestFullName = t.GuestFullName,
                GuestEmail = t.GuestEmail,
                DynamicFields = t.TicketFieldValues,
                TicketNumber = t.EventBookingTicketId,
                TicketId = t.EventTicketId,
                TicketName = t.TicketName,
                TotalTicketPrice = t.TotalPrice,
                IsPublic = t.IsPublic,
                DateOfBooking = t.CreatedDateTime.GetValueOrDefault(),
                DateOfBookingUtc = t.CreatedDateTimeUtc.GetValueOrDefault(),
                GroupName = groups.SingleOrDefault(g => g.EventGroupId == t.EventGroupId)?.GroupName
            });
        }

        public IEnumerable<EventGuestPublicView> GetPublicGuestNames(int? eventId)
        {
            var guests = BuildGuestList(eventId)
                .Where(g => g.IsPublic)
                .OrderByDescending(g => g.TicketNumber);

            return GetFirstNames(guests);
        }

        public IEnumerable<EventGuestPublicView> GetFirstNames(IEnumerable<EventGuestDetails> guests)
        {
            return guests.Select(g =>
            {
                var nameSplit = g.GuestFullName.Split(' ')
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList();

                if (nameSplit.Count == 1)
                    return new EventGuestPublicView(nameSplit[0], g.GroupName);

                if (nameSplit.Count == 2)
                {
                    return new EventGuestPublicView(
                        string.Format("{0} {1}", nameSplit[0], nameSplit[1].ToUpperInvariant().First()), 
                        g.GroupName);
                }

                if (nameSplit.Count == 3)
                {
                    return new EventGuestPublicView(
                        string.Format("{0} {1}", nameSplit[0], nameSplit[2].ToUpperInvariant().First()),
                        g.GroupName);
                }

                return null;

            }).Where(n => n != null);
        }
    }
}
