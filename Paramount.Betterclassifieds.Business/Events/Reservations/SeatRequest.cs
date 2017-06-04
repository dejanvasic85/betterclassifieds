using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatRequest
    {
        public SeatRequest(string desiredSeatNumber, IEnumerable<EventSeatBooking> seatsForDesiredTicketType)
        {
            DesiredSeatNumber = desiredSeatNumber;
            SeatsForDesiredTicketType = seatsForDesiredTicketType;
        }

        public string DesiredSeatNumber { get; }
        public IEnumerable<EventSeatBooking> SeatsForDesiredTicketType { get; }
    }
}