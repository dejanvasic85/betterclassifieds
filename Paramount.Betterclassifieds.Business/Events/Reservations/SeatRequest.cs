using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatRequest
    {
        public SeatRequest(string currentRequestId, string desiredSeat, EventSeat seat)
        {
            DesiredSeat = desiredSeat;
            CurrentRequestId = currentRequestId;
            Seat = seat;
        }

        public string CurrentRequestId { get; }
        public string DesiredSeat { get; }
        public EventSeat Seat { get; }
    }
}