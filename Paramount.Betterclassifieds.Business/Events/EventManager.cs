using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId);
        EventModel GetEventDetails(int eventId);
        EventBooking GetEventBooking(int eventBookingId);
        int GetRemainingTicketCount(int? ticketId);
        IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId);
        void ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> requests);
        TimeSpan GetRemainingTimeForReservationCollection(IEnumerable<EventTicketReservation> reservations);
        EventBooking CreateEventBooking(int eventId, ApplicationUser applicationUser, IEnumerable<EventTicketReservation> currentReservations);
        void CancelEventBooking(int? eventBookingId);
        void EventBookingPaymentCompleted(int? eventBookingId, PaymentType paymentType);
        void SetPaymentReferenceForBooking(int eventBookingId, string paymentReference, PaymentType paymentType);
        
    }

    public class EventManager : IEventManager
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly IClientConfig _clientConfig;
        private readonly EventBookingTicketFactory _eventBookingTicketFactory;

        public EventManager(IEventRepository eventRepository, IDateService dateService, IClientConfig clientConfig, EventBookingTicketFactory eventBookingTicketFactory)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _clientConfig = clientConfig;
            _eventBookingTicketFactory = eventBookingTicketFactory;
        }

        public EventModel GetEventDetailsForOnlineAdId(int onlineAdId)
        {
            return _eventRepository.GetEventDetailsForOnlineAdId(onlineAdId);
        }

        public EventModel GetEventDetails(int eventId)
        {
            return _eventRepository.GetEventDetails(eventId);
        }

        public EventBooking GetEventBooking(int eventBookingId)
        {
            return _eventRepository.GetEventBooking(eventBookingId);
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
                    Price = reservationRequest.EventTicket.Price,
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

        public EventBooking CreateEventBooking(int eventId, ApplicationUser applicationUser, IEnumerable<EventTicketReservation> currentReservations)
        {
            // Todo - create factory for this
            var reservations = currentReservations.ToList();
            var eventBooking = new EventBooking
            {
                EventId = eventId,
                CreatedDateTimeUtc = _dateService.UtcNow,
                CreatedDateTime = _dateService.Now,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
                Phone = applicationUser.Phone,
                PostCode = applicationUser.Postcode,
                UserId = applicationUser.Username,
                Status = reservations.Any(r => r.EventTicket != null && r.EventTicket.Price > 0)
                    ? EventBookingStatus.PaymentPending
                    : EventBookingStatus.Active
            };

            // Add the ticket bookings
            eventBooking.EventBookingTickets.AddRange(reservations.Select(r => _eventBookingTicketFactory.CreateFromReservation(r)));

            // Calculate the total
            eventBooking.TotalCost = reservations.Sum(r => r.Price.GetValueOrDefault() * r.Quantity);

            // Call the repository
            _eventRepository.CreateBooking(eventBooking);

            return eventBooking;
        }

        public void CancelEventBooking(int? eventBookingId)
        {
            Guard.NotNull(eventBookingId);
            var eventBooking = _eventRepository.GetEventBooking(eventBookingId.GetValueOrDefault());
            eventBooking.Status = EventBookingStatus.Cancelled;
            _eventRepository.UpdateEventBooking(eventBooking);
        }

        public void EventBookingPaymentCompleted(int? eventBookingId, PaymentType paymentType)
        {
            Guard.NotNull(eventBookingId);
            var eventBooking = _eventRepository.GetEventBooking(eventBookingId.GetValueOrDefault());
            eventBooking.Status = EventBookingStatus.Active;
            _eventRepository.UpdateEventBooking(eventBooking);
        }

        public void SetPaymentReferenceForBooking(int eventBookingId, string paymentReference, PaymentType paymentType)
        {
            var eventBooking = _eventRepository.GetEventBooking(eventBookingId);
            eventBooking.PaymentReference = paymentReference;
            eventBooking.PaymentMethod = paymentType;
            _eventRepository.UpdateEventBooking(eventBooking);
        }

        public IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId)
        {
            return _eventRepository
                .GetEventTicketReservationsForSession(sessionId)
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