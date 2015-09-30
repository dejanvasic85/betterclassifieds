using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId);
        int GetRemainingTicketCount(int? ticketId);

        EventTicketReservationResult ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> requests);
    }

    public class EventManager : IEventManager
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly IClientConfig _clientConfig;

        public EventManager(IEventRepository eventRepository, IDateService dateService, IClientConfig clientConfig)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _clientConfig = clientConfig;
        }

        public EventModel GetEventDetailsForOnlineAdId(int onlineAdId)
        {
            return _eventRepository.GetEventDetailsForOnlineAdId(onlineAdId);
        }

        public int GetRemainingTicketCount(int? ticketId)
        {
            if (!ticketId.HasValue)
                throw new ArgumentNullException("ticketId");

            var currentDate = _dateService.UtcNow;

            var ticketDetails = _eventRepository.GetEventTicketDetails(ticketId.Value, includeReservations: true);

            var reserved = ticketDetails.EventTicketReservations
                .Where(reservation => reservation.Active)
                .Where(reservation => reservation.ExpiryDateUtc > currentDate).Sum(reservation => reservation.Quantity);

            var booked = ticketDetails.EventTicketBookings.Where(b => b.Active).Sum(b => b.Quantity);

            var remainingTickets = ticketDetails.AvailableQuantity - reserved - booked;
            return remainingTickets;
        }

        public EventTicketReservationResult ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> requests)
        {
            CancelReservationsForSession(sessionId, requests);

            var eventTicketReservationResult = new EventTicketReservationResult();

            // Create reservation for each request
            foreach (var reservationRequest in requests)
            {
                var remaining = GetRemainingTicketCount(reservationRequest.EventTicket.TicketId);
                if (remaining > reservationRequest.Quantity)
                {
                    // Convert the request to a reservation
                    var eventTicketReservation = reservationRequest.ToReservation(_dateService.Now, _dateService.UtcNow, sessionId, _clientConfig.EventTicketReservationExpiryMinutes);
                    eventTicketReservationResult.Reservations.Add(eventTicketReservation);
                    _eventRepository.CreateEventTicketReservation(eventTicketReservation);
                }
                else
                {
                    eventTicketReservationResult.FailedReservations.Add(reservationRequest.ToFailedReservation());
                }
            }

            return eventTicketReservationResult;
        }

        private void CancelReservationsForSession(string sessionId, IEnumerable<EventTicketReservationRequest> requests)
        {
            // The current session needs to have all ticket reservations de-activated first
            var existingSessionReservations = _eventRepository
                .GetEventTicketReservationsForSession(sessionId)
                .Where(t => requests.Any(r => r.EventTicket.TicketId == t.TicketId));

            foreach (var existingSessionReservation in existingSessionReservations)
            {
                existingSessionReservation.Active = false;
                _eventRepository.UpdateEventTicketReservation(existingSessionReservation);
            }
        }
    }
}