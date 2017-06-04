using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventSeatingService
    {
        IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId, string orderRequestId);
        IEnumerable<EventSeatBooking> GetSeatsForTicket(EventTicket eventTicket, string orderRequestId);
        void BookSeat(int eventId, int eventBookingTicketId, string seatNumber);
    }

    public class EventSeatingService : IEventSeatingService
    {
        private readonly IEventRepository _repository;

        public EventSeatingService(IEventRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId, string orderRequestId)
        {
            return SeatFetchMediator(eventId, orderRequestId,
                () => _repository.GetEventSeats(eventId));
        }

        public IEnumerable<EventSeatBooking> GetSeatsForTicket(EventTicket eventTicket, string orderRequestId)
        {
            return SeatFetchMediator(eventTicket.EventId.GetValueOrDefault(), orderRequestId,
                () => _repository.GetEventSeatsForTicket(eventTicket.EventTicketId.GetValueOrDefault()));
        }

        public void BookSeat(int eventId, int eventBookingTicketId, string seatNumber)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<EventSeatBooking> SeatFetchMediator(int eventId, string orderRequestId, Func<IEnumerable<EventSeatBooking>> fetcher)
        {
            var reservations = _repository.GetCurrentReservationsForEvent(eventId)
                .Where(r => r.Status == EventTicketReservationStatus.Reserved)
                .Where(r => r.SessionId != orderRequestId);

            var seats = fetcher().ToDictionary(s => s.SeatNumber);

            foreach (var reservation in reservations)
            {
                seats[reservation.SeatNumber].ReservationExpiryUtc = reservation.ExpiryDateUtc;
            }

            return seats.Values;
        }
    }
}