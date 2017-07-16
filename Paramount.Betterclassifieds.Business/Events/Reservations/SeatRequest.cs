using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatRequest
    {
        public SeatRequest(string currentRequestId, EventSeat desiredSeat, IEnumerable<EventBookingTicket> bookedTickets, IEnumerable<EventTicketReservation> reservedTickets)
        {
            DesiredSeat = desiredSeat;
            BookedTickets = bookedTickets;
            ReservedTickets = reservedTickets;
            CurrentRequestId = currentRequestId;
        }

        public string CurrentRequestId { get; }
        public EventSeat DesiredSeat { get; }
        public IEnumerable<EventBookingTicket> BookedTickets { get; }
        public IEnumerable<EventTicketReservation> ReservedTickets { get; }
    }
}