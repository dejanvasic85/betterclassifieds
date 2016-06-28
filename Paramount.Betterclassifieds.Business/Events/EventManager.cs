using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public EventTicket GetEventTicket(int eventTicketId)
        {
            return _eventRepository.GetEventTicketDetails(eventTicketId);
        }

        public async Task<IEnumerable<int>> GetEventTicketsForGroup(int eventGroupId)
        {
            return await _eventRepository.GetEventTicketsForGroup(eventGroupId);
        }

        public EventBooking GetEventBooking(int eventBookingId)
        {
            return _eventRepository.GetEventBooking(eventBookingId, includeTickets: true, includeEvent: true);
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

            var eventBooking = new EventBookingFactory(_eventRepository).Create(eventId, applicationUser, currentReservations, _dateService.Now, _dateService.UtcNow);

            // Call the repository
            _eventRepository.CreateBooking(eventBooking);

            return eventBooking;
        }

        public void CancelEventBooking(int? eventBookingId)
        {
            Guard.NotNull(eventBookingId);
            var eventBooking = _eventRepository.GetEventBooking(eventBookingId.GetValueOrDefault(), includeEvent: false);
            eventBooking.Status = EventBookingStatus.Cancelled;
            _eventRepository.UpdateEventBooking(eventBooking);
        }

        public void ActivateBooking(int? eventBookingId, long? eventInvitationId)
        {
            Guard.NotNull(eventBookingId);
            var eventBooking = _eventRepository.GetEventBooking(eventBookingId.GetValueOrDefault(), includeEvent: false);
            eventBooking.Status = EventBookingStatus.Active;
            _eventRepository.UpdateEventBooking(eventBooking);

            if (eventInvitationId.HasValue)
            {
                var invitation = _eventRepository.GetEventInvitation(eventInvitationId.Value);
                if (invitation == null)
                    return;

                invitation.ConfirmedDate = _dateService.Now;
                invitation.ConfirmedDateUtc = _dateService.UtcNow;
                _eventRepository.UpdateEventInvitation(invitation);
            }
        }

        public void SetPaymentReferenceForBooking(int eventBookingId, string paymentReference, PaymentType paymentType)
        {
            var eventBooking = _eventRepository.GetEventBooking(eventBookingId, includeEvent: false);
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
                fileName: $"{eventBookingId}_.pdf",
                fileLength: ticketPdfData.Length);

            _documentRepository.Save(pdfDocument);

            var eventBooking = _eventRepository.GetEventBooking(eventBookingId, includeEvent: false);
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
            var eventBookingTickets = _eventRepository.GetEventBookingTicketsForEvent(eventId.GetValueOrDefault())
                .Where(b => b.TotalPrice > 0)
                .ToList();

            var eventModel = _eventRepository.GetEventDetails(eventId.GetValueOrDefault());
            var totalSales = eventBookingTickets.Sum(t => t.Price.GetValueOrDefault());
            var totalTicketQty = eventBookingTickets.Count;
            var paymentSummary = new EventPaymentSummary
            {
                TotalTicketSalesAmount = totalSales,
                EventOrganiserOwedAmount = totalSales
            };

            if (!eventModel.IncludeTransactionFee.GetValueOrDefault())
            {
                var totalFees = new TicketFeeCalculator(_clientConfig).GetFeeTotalForOrganiserForAllTicketSales(totalSales, totalTicketQty);
                paymentSummary.EventOrganiserOwedAmount -= totalFees;
                paymentSummary.EventOrganiserFeesTotalFeesAmount = totalFees;
            }

            return paymentSummary;
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

        public EventInvitation GetEventInvitation(long eventInvitationId)
        {
            return _eventRepository.GetEventInvitation(eventInvitationId);
        }

        public void CloseEvent(int eventId)
        {
            var eventModel = _eventRepository.GetEventDetails(eventId);
            eventModel.ClosingDate = DateTime.Now;
            eventModel.ClosingDateUtc = DateTime.UtcNow;
            _eventRepository.UpdateEvent(eventModel);
        }

        public void UpdateEventDetails(int adId, int eventId, string title, string description, string htmlText,
            DateTime eventStartDate, DateTime eventEndDateTime, string location, decimal? locationLatitude,
            decimal? locationLongitude, string organiserName, string organiserPhone, DateTime adStartDate,
            string floorPlanDocumentId, string locationFloorPlanFilename, Address address)
        {
            var originalEventDetails = _eventRepository.GetEventDetails(eventId);
            var onlineAd = _bookingManager.GetOnlineAd(adId);

            if (originalEventDetails == null || onlineAd == null)
                throw new ArgumentException("Cannot find required event to update", "eventId");

            if (IsEventEditable(eventId))
            {
                // Only the following details will allowed to be changed if the event has started
                originalEventDetails.EventStartDate = eventStartDate;
                originalEventDetails.EventEndDate = eventEndDateTime;
                originalEventDetails.Location = location;


                if (locationLatitude.HasValue && originalEventDetails.LocationLatitude != locationLatitude &&
                    locationLongitude.HasValue && originalEventDetails.LocationLongitude != locationLongitude)
                {
                    originalEventDetails.LocationLatitude = locationLatitude;
                    originalEventDetails.LocationLongitude = locationLongitude;

                    // Only perform this in non-debug scenario so that we don't have to waste the timezone look ups for development environments
#if !DEBUG
                    // Update the timezone info using the location service
                    var timezoneResult = _locationService.GetTimezone(locationLatitude.Value, locationLongitude.Value);
                    originalEventDetails.TimeZoneId = timezoneResult.TimeZoneId;
                    originalEventDetails.TimeZoneName = timezoneResult.TimeZoneName;
                    originalEventDetails.TimeZoneDaylightSavingsOffsetSeconds = timezoneResult.DstOffset;
                    originalEventDetails.TimeZoneUtcOffsetSeconds = timezoneResult.RawOffset;
#endif
                    address.AddressId = originalEventDetails.AddressId;
                    originalEventDetails.Address = address;
                    _eventRepository.UpdateEventAddress(address);
                }

                onlineAd.Heading = title; // This is used for ticketing so cannot change!
            }

            onlineAd.Description = description;
            onlineAd.HtmlText = htmlText;
            onlineAd.ContactName = organiserName;
            onlineAd.ContactPhone = organiserPhone;
            originalEventDetails.LocationFloorPlanDocumentId = floorPlanDocumentId;
            originalEventDetails.LocationFloorPlanFilename = locationFloorPlanFilename;

            _bookingManager.UpdateOnlineAd(adId, onlineAd);
            _eventRepository.UpdateEvent(originalEventDetails);
            _bookingManager.UpdateSchedule(adId, adStartDate);
        }

        public EventBookingTicketValidation GetTicketValidation(int eventTicketId)
        {
            return _eventRepository.GetEventBookingTicketValidation(eventTicketId);
        }

        public EventInvitation CreateInvitationForUserNetwork(int eventId, int userNetworkId)
        {
            var eventInvitation = new EventInvitation
            {
                EventId = eventId,
                UserNetworkId = userNetworkId,
                CreatedDate = _dateService.Now,
                CreatedDateUtc = _dateService.UtcNow
            };

            _eventRepository.CreateEventInvitation(eventInvitation);

            return eventInvitation;
        }

        public async Task<IEnumerable<EventGroup>> GetEventGroups(int eventId, int? eventTicketId = null)
        {
            return await _eventRepository.GetEventGroups(eventId, eventTicketId);
        }

        public async Task<EventGroup> GetEventGroup(int eventGroupId)
        {
            return await _eventRepository.GetEventGroup(eventGroupId);
        }

        public void AssignGroupToTicket(int eventBookingTicketId, int? eventGroupId)
        {
            // Update the event booking ticket
            var eventBookingTicket = _eventRepository.GetEventBookingTicket(eventBookingTicketId);
            Guard.NotNull(eventBookingTicket);

            eventBookingTicket.EventGroupId = eventGroupId;
            _eventRepository.UpdateEventBookingTicket(eventBookingTicket);
        }

        public void AddEventGroup(int eventId, string groupName, int? maxGuests, IEnumerable<int> tickets, string createdByUser)
        {
            // Create the new event group
            var eventGroup = new EventGroup
            {
                EventId = eventId,
                CreatedDateTime = _dateService.Now,
                CreatedDateTimeUtc = _dateService.UtcNow,
                CreatedBy = createdByUser,
                GroupName = groupName,
                MaxGuests = maxGuests,
                AvailableToAllTickets = tickets == null                
            };

            _eventRepository.CreateEventGroup(eventGroup, tickets);
        }
    }
}