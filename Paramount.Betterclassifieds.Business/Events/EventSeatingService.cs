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
        void RemoveSeatBooking(EventBookingTicket eventBookingTicket);
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

            var eventSeat = GetEventSeat(eventTicketId, seatNumber);
            if (eventSeat.EventBookingTicketId.HasValue)
            {
                _logService.Warn($"The event seat {seatNumber} has alright been assigned to eventBookingTicketId {eventSeat.EventBookingTicketId}");
            }
            eventSeat.EventBookingTicketId = eventBookingTicketId;

            _repository.UpdateEventSeat(eventSeat);
        }

        public void RemoveSeatBooking(EventBookingTicket eventBookingTicket)
        {
            Guard.NotNull(eventBookingTicket);
            Guard.NotNullOrEmpty(eventBookingTicket.SeatNumber);

            var eventSeat = GetEventSeat(eventBookingTicket.EventTicketId, eventBookingTicket.SeatNumber);

            eventSeat.EventBookingTicketId = null;
            _repository.UpdateEventSeat(eventSeat);
        }

        private EventSeatBooking GetEventSeat(int eventTicketId, string seatNumber)
        {
            var eventSeat = _repository.GetEventSeat(eventTicketId, seatNumber);

            if (eventSeat == null)
            {
                throw new NullReferenceException($"Event seat cannot be found for ticket id [{eventTicketId}] seat Number [{seatNumber}]");
            }

            return eventSeat;
        }

        private IEnumerable<EventSeatBooking> SeatFetchMediator(int eventId, string orderRequestId, Func<IEnumerable<EventSeatBooking>> fetcher)
        {
            // Retrieves the current reservations for the event that is NOT for the current session
            // Just so that the reservation expiry can be set on the seats coming back from the eventSeatBooking table.

            // TODO : Kill this code along with the ReservationExpiryUtc field and EventBookingTicketId field on the eventSeat 
            // And this problem should just go away. 
            var reservations = _repository.GetCurrentReservationsForEvent(eventId)
                .Where(r => r.Status == EventTicketReservationStatus.Reserved)
                .Where(r => r.SessionId != orderRequestId);

            var seats = fetcher().ToDictionary(s => s.SeatNumber);

            foreach (var reservation in reservations)
            {
                try
                {
                    seats[reservation.SeatNumber].ReservationExpiryUtc = reservation.ExpiryDateUtc;
                }
                catch (Exception ex)
                {
                    var seatsStr = string.Join(",", seats.Select(s => s.Key).ToArray());
                    _logService.Error($"The seat number [{reservation.SeatNumber}] is not within the reservation seats: [{seatsStr}]. Please investigate!", ex);
                }
            }

            return seats.Values;
        }
    }
}