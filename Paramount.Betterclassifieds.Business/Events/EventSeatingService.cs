using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventSeatingService
    {
        IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId, string sessionId);
    }

    public class EventSeatingService : IEventSeatingService
    {
        private readonly IEventRepository _repository;

        public EventSeatingService(IEventRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId, string sessionId)
        {
            var reservations = _repository.GetCurrentReservationsForEvent(eventId)
                .Where(r => r.Status == EventTicketReservationStatus.Reserved)
                .Where(r => r.SessionId != sessionId);

            var seats = _repository.GetEventSeats(eventId).ToDictionary(s => s.SeatNumber);

            foreach (var reservation in reservations)
            {
                seats[reservation.SeatNumber].ReservationExpiryUtc = reservation.ExpiryDateUtc;
            }

            return seats.Values;
        }

        public void BookSeat(int eventId, string seatNumber, int eventBookingId)
        {
            // Todo
            // var seat = _repository.GetEventSeatByNumber(eventId, seatNumber);
        }
    }
}