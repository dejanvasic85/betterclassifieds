using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Location;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventManager : IEventManager
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly IClientConfig _clientConfig;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBookingManager _bookingManager;
        private readonly ILocationService _locationService;

        public EventManager(IEventRepository eventRepository, IDateService dateService, IClientConfig clientConfig, IDocumentRepository documentRepository, IBookingManager bookingManager, ILocationService locationService)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _clientConfig = clientConfig;
            _documentRepository = documentRepository;
            _bookingManager = bookingManager;
            _locationService = locationService;
        }

        public EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false)
        {
            return _eventRepository.GetEventDetailsForOnlineAdId(onlineAdId, includeBookings);
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

            var eventTicket = _eventRepository.GetEventTicketDetails(ticketId.GetValueOrDefault(), includeReservations: true);

            return GetRemainingTicketCount(eventTicket);
        }

        public int GetRemainingTicketCount(EventTicket ticketDetails)
        {
            var reserved = ticketDetails.EventTicketReservations
                .Where(reservation => reservation.Status == EventTicketReservationStatus.Reserved)
                .Where(reservation => reservation.ExpiryDateUtc > _dateService.UtcNow).Sum(reservation => reservation.Quantity);

            var remainingTickets = ticketDetails.RemainingQuantity - reserved;
            return remainingTickets;
        }

        public void ReserveTickets(string sessionId, IEnumerable<EventTicketReservation> reservations)
        {
            CancelReservationsForSession(sessionId);

            // Create reservation for each request
            foreach (var r in reservations)
            {
                _eventRepository.CreateEventTicketReservation(r);
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
            Guard.NotDefaultValue(eventId);
            Guard.NotNull(applicationUser);

            var eventBooking = new EventBookingFactory().Create(eventId, applicationUser, currentReservations, _dateService.Now, _dateService.UtcNow);

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
                eventTicket.RemainingQuantity = eventTicket.RemainingQuantity - 1;
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

        public void CreateEventTicket(int eventId, string ticketName, decimal price, int remainingQuantity)
        {
            Guard.NotDefaultValue(eventId);
            var ticket = new EventTicketFactory().Create(remainingQuantity, eventId, ticketName, price);
            _eventRepository.CreateEventTicket(ticket);
        }

        public IEnumerable<EventGuestDetails> BuildGuestList(int? eventId)
        {
            Guard.NotNull(eventId);
            var eventModel = _eventRepository.GetEventDetails(eventId.GetValueOrDefault());
            var tickets = _eventRepository.GetEventBookingTicketsForEvent(eventId);

            return tickets.Select(t => new EventGuestDetails
            {
                GuestFullName = t.GuestFullName,
                GuestEmail = t.GuestEmail,
                DynamicFields = t.TicketFieldValues,
                BarcodeData = new TicketBarcodeService().Generate(eventModel, t),
                TicketNumber = t.EventBookingTicketId,
                TicketName = t.TicketName
            });
        }

        public EventPaymentSummary BuildPaymentSummary(int? eventId)
        {
            Guard.NotNull(eventId);
            var eventBookings = _eventRepository.GetEventBookingsForEvent(eventId.GetValueOrDefault());

            var totalSales = eventBookings.Sum(e => e.TotalCost);
            var ticketFee = _clientConfig.EventTicketFee; // Stored as a whole number
            decimal totalTicketFee = 0;

            // Make sure that the configured ticket fee is within 0.1 and 100 so that we can divide it
            if (ticketFee != 0 && ticketFee > 0 && ticketFee <= 100)
            {
                totalTicketFee = totalSales * (ticketFee / 100);
            }

            var organiserPaymentAmount = totalSales - totalTicketFee;

            return new EventPaymentSummary
            {
                TotalTicketSalesAmount = totalSales,
                SystemTicketFee = ticketFee,
                EventOrganiserOwedAmount = organiserPaymentAmount
            };
        }

        /// <summary>
        /// Returns true if no tickets have been booked
        /// </summary>
        public bool IsEventEditable(int? eventId)
        {
            Guard.NotNull(eventId);
            return !_eventRepository.GetEventBookingsForEvent(eventId.GetValueOrDefault()).Any();
        }

        public void CreateEventPaymentRequest(int eventId, PaymentType paymentType, decimal requestedAmount, string requestedByUser)
        {
            Guard.NotDefaultValue(eventId);
            Guard.NotDefaultValue(requestedAmount);

            if (paymentType == PaymentType.None || paymentType == PaymentType.CreditCard)
            {
                throw new ArgumentException("Payment cannot be set to none.", "paymentType");
            }

            var request = new EventPaymentRequestFactory()
                .Create(eventId, paymentType, requestedAmount, requestedByUser, _dateService.Now, _dateService.UtcNow);

            _eventRepository.CreateEventPaymentRequest(request);
        }

        public EventPaymentRequestStatus GetEventPaymentRequestStatus(int? eventId)
        {
            Guard.NotNull(eventId);
            var eventPaymentRequest = _eventRepository.GetEventPaymentRequestForEvent(eventId.GetValueOrDefault());
            var eventModel = _eventRepository.GetEventDetails(eventId.GetValueOrDefault());

            if (eventPaymentRequest != null)
            {
                if (eventPaymentRequest.IsPaymentProcessed.HasValue && eventPaymentRequest.IsPaymentProcessed.Value)
                {
                    return EventPaymentRequestStatus.Complete;
                }

                return EventPaymentRequestStatus.PaymentPending;
            }

            if (eventModel.Tickets == null || !eventModel.Tickets.Any() || eventModel.Tickets.All(t => t.Price <= 0))
            {
                return EventPaymentRequestStatus.NotAvailable;
            }

            if (eventModel.ClosingDateUtc == null || eventModel.ClosingDateUtc > _dateService.UtcNow)
            {
                return EventPaymentRequestStatus.CloseEventFirst;
            }

            return EventPaymentRequestStatus.RequestPending;
        }

        public void CloseEvent(int eventId)
        {
            var eventModel = _eventRepository.GetEventDetails(eventId);
            eventModel.ClosingDate = DateTime.Now;
            eventModel.ClosingDateUtc = DateTime.UtcNow;
            _eventRepository.UpdateEvent(eventModel);
        }

        public void UpdateEventDetails(int adId, int eventId, string title, string description, string htmlText, DateTime eventStartDate, DateTime eventEndDateTime, string location, decimal? locationLatitude, decimal? locationLongitude, string organiserName, string organiserPhone, DateTime adStartDate)
        {
            var originalEventDetails = _eventRepository.GetEventDetails(eventId);
            var onlineAd = _bookingManager.GetOnlineAd(adId);

            if (originalEventDetails == null || onlineAd == null)
                throw new ArgumentException("Cannot find required event to update", "eventId");

            if (IsEventEditable(eventId))
            {
                originalEventDetails.EventStartDate = eventStartDate;
                originalEventDetails.EventEndDate = eventEndDateTime;
                originalEventDetails.Location = location;

                if (locationLatitude.HasValue && originalEventDetails.LocationLatitude != locationLatitude &&
                    locationLongitude.HasValue && originalEventDetails.LocationLongitude != locationLongitude)
                {
                    // Update the timezone info using the location service
                    var timezoneResult = _locationService.GetTimezone(locationLatitude.Value, locationLongitude.Value);
                    originalEventDetails.TimeZoneId = timezoneResult.TimeZoneId;
                    originalEventDetails.TimeZoneName = timezoneResult.TimeZoneName;
                    originalEventDetails.TimeZoneDaylightSavingsOffsetSeconds = timezoneResult.DstOffset;
                    originalEventDetails.TimeZoneUtcOffsetSeconds = timezoneResult.RawOffset;
                }

                originalEventDetails.LocationLatitude = locationLatitude;
                originalEventDetails.LocationLongitude = locationLongitude;
                onlineAd.Heading = title;
            }

            onlineAd.Description = description;
            onlineAd.HtmlText = htmlText;
            onlineAd.ContactName = organiserName;
            onlineAd.ContactPhone = organiserPhone;


            _bookingManager.UpdateOnlineAd(adId, onlineAd);
            _eventRepository.UpdateEvent(originalEventDetails);
            _bookingManager.UpdateSchedule(adId, adStartDate);
        }
    }


}