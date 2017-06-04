using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatRequest
    {
        public SeatRequest(string desiredSeatNumber, IEnumerable<EventSeatBooking> reservedSeats)
        {
            DesiredSeatNumber = desiredSeatNumber;
            ReservedSeats = reservedSeats;
        }

        public string DesiredSeatNumber { get; }
        public IEnumerable<EventSeatBooking> ReservedSeats { get; }
    }
}