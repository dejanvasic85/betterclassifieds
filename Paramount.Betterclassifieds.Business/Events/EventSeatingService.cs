using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventSeatingService
    {
        IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId);
    }

    public class EventSeatingService : IEventSeatingService
    {
        private readonly IEventRepository _repository;

        public EventSeatingService(IEventRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId)
        {
            return _repository.GetEventSeats(eventId);
        }

        public void BookSeat(int eventId, string seatNumber, int eventBookingId)
        {
            // Todo
            // var seat = _repository.GetEventSeatByNumber(eventId, seatNumber);
        }
    }
}