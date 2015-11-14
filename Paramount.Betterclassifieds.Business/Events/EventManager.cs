using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.DocumentStorage;
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
        void AdjustRemainingQuantityAndCancelReservations(string sessionId, IList<EventBookingTicket> eventBookingTickets);
        string CreateEventTicketsDocument(int eventBookingId, byte[] ticketPdfData, DateTime? ticketsSentDate = null);
        void UpdateEventTicket(int eventTicketId, string ticketName, decimal price, int remainingQuantity);
        void CreateEventTicket(int? eventId, string ticketName, decimal price, int remainingQuantity);
    }

    public class EventManager : IEventManager
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly IClientConfig _clientConfig;
        private readonly EventBookingTicketFactory _eventBookingTicketFactory;
        private readonly IDocumentRepository _documentRepository;

        public EventManager(IEventRepository eventRepository, IDateService dateService, IClientConfig clientConfig, EventBookingTicketFactory eventBookingTicketFactory, IDocumentRepository documentRepository)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _clientConfig = clientConfig;
            _eventBookingTicketFactory = eventBookingTicketFactory;
            _documentRepository = documentRepository;
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
            return _eventRepository.GetEventBooking(eventBookingId, includeTickets: true);
        }

        public int GetRemainingTicketCount(int? ticketId)
        {
            Guard.NotNull(ticketId);

            var currentDate = _dateService.UtcNow;

            var ticketDetails = _eventRepository.GetEventTicketDetails(ticketId.GetValueOrDefault(), includeReservations: true);

            var reserved = ticketDetails.EventTicketReservations
                .Where(reservation => reservation.Status == EventTicketReservationStatus.Reserved)
                .Where(reservation => reservation.ExpiryDateUtc > currentDate).Sum(reservation => reservation.Quantity);

            var remainingTickets = ticketDetails.RemainingQuantity - reserved;
            return remainingTickets;
        }

        public void ReserveTickets(string sessionId, IEnumerable<EventTicketReservationRequest> requests)
        {
            var requestsData = requests.ToArray();

            CancelReservationsForSession(sessionId);

            // Create reservation for each request
            foreach (var reservationRequest in requestsData)
            {
                Guard.NotNull(reservationRequest.EventTicket);
                Guard.NotNull(reservationRequest.EventTicket.EventTicketId);

                var reservation = new EventTicketReservation
                {
                    CreatedDate = _dateService.Now,
                    CreatedDateUtc = _dateService.UtcNow,
                    ExpiryDate = _dateService.Now.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                    ExpiryDateUtc = _dateService.UtcNow.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                    SessionId = sessionId,
                    Quantity = reservationRequest.Quantity,
                    EventTicketId = reservationRequest.EventTicket.EventTicketId.GetValueOrDefault(),
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

        public void AdjustRemainingQuantityAndCancelReservations(string sessionId, IList<EventBookingTicket> eventBookingTickets)
        {
            CancelReservationsForSession(sessionId);

            foreach (var eventBookingTicket in eventBookingTickets)
            {
                var eventTicket = _eventRepository.GetEventTicketDetails(eventBookingTicket.EventTicketId);
                eventTicket.RemainingQuantity = eventTicket.RemainingQuantity - eventBookingTicket.Quantity;
                _eventRepository.UpdateEventTicket(eventTicket);
            }
        }

        public string CreateEventTicketsDocument(int eventBookingId, byte[] ticketPdfData, DateTime? ticketsSentDate = null)
        {
            var pdfDocument = new Document(Guid.NewGuid(), ticketPdfData, ContentType.Pdf,
                fileName: string.Format("{0}_.pdf", eventBookingId),
                fileLength: ticketPdfData.Length);

            _documentRepository.Save(pdfDocument);

            var eventBooking = _eventRepository.GetEventBooking(eventBookingId);
            eventBooking.TicketsDocumentId = pdfDocument.DocumentId;
            if (ticketsSentDate.HasValue)
            {
                eventBooking.TicketsSentDate = ticketsSentDate;
                eventBooking.TicketsSentDateUtc = ticketsSentDate.Value.ToUniversalTime();
            }
            _eventRepository.UpdateEventBooking(eventBooking);

            return pdfDocument.DocumentId.ToString();
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

        public void UpdateEventTicket(int eventTicketId, string ticketName, decimal price, int remainingQuantity)
        {
            var eventTicket = _eventRepository.GetEventTicketDetails(eventTicketId);
            eventTicket.TicketName = ticketName;
            eventTicket.Price = price;
            if (eventTicket.RemainingQuantity != remainingQuantity)
            {
                var difference = remainingQuantity - eventTicket.RemainingQuantity;
                eventTicket.AvailableQuantity += difference;
                eventTicket.RemainingQuantity = remainingQuantity;
            }

            _eventRepository.UpdateEventTicket(eventTicket);
        }

        public void CreateEventTicket(int? eventId, string ticketName, decimal price, int remainingQuantity)
        {
            var ticket = new EventTicket
            {
                AvailableQuantity = remainingQuantity,
                RemainingQuantity = remainingQuantity,
                EventId = eventId,
                TicketName = ticketName,
                Price = price
            };
            _eventRepository.CreateEventTicket(ticket);
        }
    }
}