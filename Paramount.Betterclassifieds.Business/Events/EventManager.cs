using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetails(int onlineAdId);
        EventTicketReservationResult ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> request);
    }

    public class EventManager : IEventManager
    {
        private readonly IEventRepository _eventRepository;

        public EventManager(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventModel GetEventDetails(int onlineAdId)
        {
            return _eventRepository.GetEventDetails(onlineAdId);
        }

        public EventTicketReservationResult ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> request)
        {
            var eventTicketReservationResult = new EventTicketReservationResult();

            // The current session needs to have all ticket reservations de-activated first
            var existingSessionReservations = _eventRepository
                .GetEventTicketReservationsForSession(sessionId, false)
                .Where(t => request.Any(r => r.EventTicket.TicketId == t.TicketId));

            foreach (var existingSessionReservation in existingSessionReservations)
            {
                existingSessionReservation.Active = false;
                _eventRepository.UpdateEventTicketReservation(existingSessionReservation);
            }

            // Create reservation for each request
            foreach (var reservationRequest in request)
            {

            }

            return eventTicketReservationResult;
        }
    }
}