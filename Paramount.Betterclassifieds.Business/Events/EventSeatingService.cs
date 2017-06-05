using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventSeatingService
    {
        IEnumerable<EventSeatBooking> GetSeatsForEvent(int eventId, string orderRequestId);
        IEnumerable<EventSeatBooking> GetSeatsForTicket(EventTicket eventTicket, string orderRequestId);
        void BookSeat(int eventTicketId, int eventBookingTicketId, string seatNumber);
    }

    public class EventSeatingService : IEventSeatingService
    {
        private readonly IEventRepository _repository;
        private readonly ILogService _logService;

        public EventSeatingService(IEventRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
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

        public void BookSeat(int eventTicketId, int eventBookingTicketId, string seatNumber)
        {
            _logService.Info($"Booking seat [{seatNumber}] for eventTicketId [{eventTicketId}] eventBookingTicketID [{eventBookingTicketId}]");

            var eventSeat = _repository.GetEventSeat(eventTicketId, seatNumber);

            if (eventSeat == null)
            {
                throw new NullReferenceException($"Event seat cannot be found for ticket id [{eventTicketId}] seat Number [{seatNumber}]");
            }

            eventSeat.EventBookingTicketId = eventBookingTicketId;

            _repository.UpdateEventSeat(eventSeat);
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