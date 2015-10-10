using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId);
        EventModel GetEventDetails(int eventId);
        int GetRemainingTicketCount(int? ticketId);
        IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId);
        void ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> requests);
        TimeSpan GetRemainingTimeForReservationCollection(IEnumerable<EventTicketReservation> reservations);
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

        public EventModel GetEventDetails(int eventId)
        {
            return _eventRepository.GetEventDetails(eventId);
        }

        public int GetRemainingTicketCount(int? ticketId)
        {
            if (!ticketId.HasValue)
                throw new ArgumentNullException("ticketId");

            var currentDate = _dateService.UtcNow;

            var ticketDetails = _eventRepository.GetEventTicketDetails(ticketId.Value, includeReservations: true);

            var reserved = ticketDetails.EventTicketReservations
                .Where(reservation => reservation.Status == EventTicketReservationStatus.Reserved)
                .Where(reservation => reservation.ExpiryDateUtc > currentDate).Sum(reservation => reservation.Quantity);

            //var booked = ticketDetails.EventTicketBookings.Where(b => b.Active).Sum(b => b.Quantity);

            var remainingTickets = ticketDetails.RemainingQuantity - reserved;// - booked;
            return remainingTickets;
        }

        public void ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> requests)
        {
            var requestsData = requests.ToArray();

            CancelReservationsForSession(sessionId);

            // Create reservation for each request
            foreach (var reservationRequest in requestsData)
            {
                if (reservationRequest.EventTicket == null || !reservationRequest.EventTicket.EventTicketId.HasValue)
                {
                    throw new ArgumentNullException("requests", "Event or Ticket ID are null. Unable to proceed");
                }

                var reservation = new EventTicketReservation
                {
                    CreatedDate = _dateService.Now,
                    CreatedDateUtc = _dateService.UtcNow,
                    ExpiryDate = _dateService.Now.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                    ExpiryDateUtc = _dateService.UtcNow.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                    SessionId = sessionId,
                    Quantity = reservationRequest.Quantity,
                    EventTicketId = reservationRequest.EventTicket.EventTicketId.Value,
                    Status = new SufficientTicketsRule()
                        .IsSatisfiedBy(new RemainingTicketsWithRequestInfo(reservationRequest.Quantity, GetRemainingTicketCount(reservationRequest.EventTicket.EventTicketId)))
                        .Result,
                };

                _eventRepository.CreateEventTicketReservation(reservation);
            }
        }

        public TimeSpan GetRemainingTimeForReservationCollection(IEnumerable<EventTicketReservation> reservations)
        {
            var soonestEnding = reservations.OrderBy(r => r.ExpiryDateUtc).FirstOrDefault();
            if (soonestEnding == null || !soonestEnding.ExpiryDateUtc.HasValue)
            {
                return new TimeSpan();
            }

            return soonestEnding.ExpiryDateUtc.Value - _dateService.UtcNow;
        }

        public IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId)
        {
            return _eventRepository.GetEventTicketReservationsForSession(sessionId)
                .Where(e => e.Status != EventTicketReservationStatus.Cancelled);
        }

        private void CancelReservationsForSession(string sessionId)
        {
            // The current session needs to have all ticket reservations de-activated first
            var existingSessionReservations = _eventRepository.GetEventTicketReservationsForSession(sessionId);

            foreach (var existingSessionReservation in existingSessionReservations)
            {
                existingSessionReservation.Status = EventTicketReservationStatus.Cancelled;
                _eventRepository.UpdateEventTicketReservation(existingSessionReservation);
            }
        }
    }
}