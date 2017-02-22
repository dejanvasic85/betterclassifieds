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
        private readonly IUserManager _userManager;
        private readonly IEventBarcodeValidator _eventBarcodeValidator;
        private readonly IBarcodeGenerator _barcodeGenerator;
        private readonly ILogService _logService;

        public EventManager(IEventRepository eventRepository, IDateService dateService, IClientConfig clientConfig, IDocumentRepository documentRepository, IBookingManager bookingManager, ILocationService locationService, IUserManager userManager, IEventBarcodeValidator eventBarcodeValidator, IBarcodeGenerator barcodeGenerator, ILogService logService)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _clientConfig = clientConfig;
            _documentRepository = documentRepository;
            _bookingManager = bookingManager;
            _locationService = locationService;
            _userManager = userManager;
            _eventBarcodeValidator = eventBarcodeValidator;
            _barcodeGenerator = barcodeGenerator;
            _logService = logService;
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

        public EventTicket GetEventTicketAndReservations(int eventTicketId)
        {
            return _eventRepository.GetEventTicketDetails(eventTicketId, true);
        }

        public async Task<IEnumerable<EventTicket>> GetEventTicketsForGroup(int eventGroupId)
        {
            var eventGroup = await _eventRepository.GetEventGroup(eventGroupId);
            if (eventGroup == null)
            {
                return null;
            }

            if (eventGroup.AvailableToAllTickets.GetValueOrDefault())
            {
                return await _eventRepository.GetEventTickets(eventGroup.EventId.GetValueOrDefault());
            }

            return await _eventRepository.GetEventTicketsForGroup(eventGroupId);
        }

        public EventBooking GetEventBooking(int eventBookingId)
        {
            return _eventRepository.GetEventBooking(eventBookingId, includeTickets: true, includeEvent: true);
        }

        public EventBookingTicket GetEventBookingTicket(int eventBookingTicketId)
        {
            return _eventRepository.GetEventBookingTicket(eventBookingTicketId);
        }

        public EventBookingTicket UpdateEventBookingTicket(int eventBookingTicketId, string guestFullName, string guestEmail, int? eventGroupId, bool isPublic, IEnumerable<EventBookingTicketField> fields, Func<string, string> barcodeUrlCreator)
        {
            var eventBookingTicket = _eventRepository.GetEventBookingTicket(eventBookingTicketId);
            if (eventBookingTicket == null)
                throw new ArgumentException($"eventBookingTicket {eventBookingTicketId} not found");

            var eventBooking = _eventRepository.GetEventBooking(eventBookingTicket.EventBookingId);

            var newEventBookingTicket = new EventBookingTicketFactory(_eventRepository, _dateService)
                .CreateFromExisting(eventBookingTicket, guestFullName, guestEmail, isPublic, eventGroupId, fields, _userManager.GetCurrentUser().Username);

            // Save the new ticket
            _eventRepository.CreateEventBookingTicket(newEventBookingTicket);

            // Now we have the id so we need to generate the barcode
            CreateTicketBarcodeAndUpdate(eventBooking, newEventBookingTicket, barcodeUrlCreator);


            // We always mark the existing event booking ticket as inactive and simply create a new one
            // But we also need to reset the cost of the ticket!
            eventBookingTicket.IsActive = false;
            eventBookingTicket.Price = 0;
            eventBookingTicket.TotalPrice = 0;
            eventBookingTicket.TransactionFee = 0;
            _eventRepository.UpdateEventBookingTicket(eventBookingTicket);

            return newEventBookingTicket;
        }

        private void CreateTicketBarcodeAndUpdate(EventBooking eventBooking,
            EventBookingTicket newEventBookingTicket, Func<string, string> barcodeUrlCreator)
        {
            var barcodeData = barcodeUrlCreator(_eventBarcodeValidator.GetDataForBarcode(eventBooking.EventId, newEventBookingTicket));
            var barcode = _barcodeGenerator.CreateQr(barcodeData, height: 250, width: 250);
            var document = new Document(Guid.NewGuid(), barcode, ContentType.Gif,
                $"Ticket-{newEventBookingTicket.EventBookingTicketId}.gif", barcode.Length);
            _documentRepository.Create(document);
            newEventBookingTicket.BarcodeImageDocumentId = document.DocumentId;
            _eventRepository.UpdateEventBookingTicket(newEventBookingTicket);
        }

        public EventBookingTicket CancelEventBookingTicket(int eventBookingTicketId)
        {
            var eventBookingTicket = _eventRepository.GetEventBookingTicket(eventBookingTicketId);

            if (eventBookingTicket == null)
                throw new ArgumentException($"eventBookingTicket {eventBookingTicketId} not found");

            eventBookingTicket.IsActive = false;
            eventBookingTicket.LastModifiedBy = _userManager.GetCurrentUser().Username;
            eventBookingTicket.LastModifiedDate = _dateService.Now;
            eventBookingTicket.LastModifiedDateUtc = _dateService.UtcNow;
            _eventRepository.UpdateEventBookingTicket(eventBookingTicket);

            var eventBooking = _eventRepository.GetEventBooking(eventBookingTicket.EventBookingId, includeTickets: true);
            if (eventBooking.EventBookingTickets.Count(t => t.IsActive) <= 0)
            {
                eventBooking.Status = EventBookingStatus.Cancelled;
                _eventRepository.UpdateEventBooking(eventBooking);
            }

            // Increase the remaining quantity on the ticket type !
            var ticket = _eventRepository.GetEventTicketDetails(eventBookingTicket.EventTicketId);
            ticket.RemainingQuantity = ticket.RemainingQuantity + 1;
            _eventRepository.UpdateEventTicket(ticket);

            return eventBookingTicket;
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

            // CreateRepository reservation for each request
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

        public EventBooking CreateEventBooking(int eventId, ApplicationUser applicationUser, IEnumerable<EventTicketReservation> currentReservations, Func<string, string> barcodeUrlCreator)
        {
            Guard.NotDefaultValue(eventId);
            Guard.NotNull(applicationUser);

            var eventBooking = new EventBookingFactory(_eventRepository, _dateService).Create(eventId, applicationUser, currentReservations);
            _eventRepository.CreateBooking(eventBooking);
            _logService.Info("Event booking created. Id " + eventBooking.EventBookingId);

            if (eventBooking.EventBookingTickets.Count > 0 && barcodeUrlCreator == null)
                throw new NullReferenceException("barcodeUrlCreator cannot be null when there's tickets to have their barcode images created.");

            Parallel.ForEach(eventBooking.EventBookingTickets, ticket =>
            {
                try
                {
                    _logService.Info("Creating barcode for ticket " + ticket.EventBookingTicketId);
                    CreateTicketBarcodeAndUpdate(eventBooking, ticket, barcodeUrlCreator);
                }
                catch (Exception ex)
                {
                    _logService.Error("Unable to generate barcode for ticket " + ticket.EventBookingTicketId, ex);
                    throw;
                }
            });

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

            // This booking may have been activated through an 'invite'
            // So mark the invitation as confirmed as well
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

        public string CreateEventTicketDocument(int eventBookingTicketId, byte[] ticketPdfData)
        {
            var pdfDocument = new Document(Guid.NewGuid(), ticketPdfData, ContentType.Pdf,
                fileName: $"Ticket_{eventBookingTicketId}.pdf",
                fileLength: ticketPdfData.Length);

            _documentRepository.Create(pdfDocument);

            var eventBookingTicket = _eventRepository.GetEventBookingTicket(eventBookingTicketId);
            eventBookingTicket.TicketDocumentId = pdfDocument.DocumentId;
            _eventRepository.UpdateEventBookingTicket(eventBookingTicket);

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

        public void UpdateEventTicket(int eventTicketId, string ticketName, decimal price, int remainingQuantity, bool isActive, IEnumerable<EventTicketField> fields)
        {
            var eventTicket = _eventRepository.GetEventTicketDetails(eventTicketId);
            var nameChanged = eventTicket.TicketName != ticketName;
            eventTicket.TicketName = ticketName;
            eventTicket.Price = price;
            eventTicket.IsActive = isActive;

            if (eventTicket.RemainingQuantity != remainingQuantity)
            {
                var difference = remainingQuantity - eventTicket.RemainingQuantity;
                eventTicket.AvailableQuantity += difference; // This makes the total available tickets add the difference
                eventTicket.RemainingQuantity = remainingQuantity;
            }

            if (fields != null)
            {
                eventTicket.EventTicketFields = fields.ToList();
            }

            _eventRepository.UpdateEventTicketIncudingFields(eventTicket);

            if (nameChanged)
            {
                _eventRepository.UpdateEventBookingTicketNames(eventTicket.EventTicketId.GetValueOrDefault(), ticketName);
            }
        }

        public EventTicket CreateEventTicket(int eventId, string ticketName, decimal price, int remainingQuantity, bool isActive, IEnumerable<EventTicketField> fields)
        {
            Guard.NotDefaultValue(eventId);
            var ticket = new EventTicketFactory().Create(remainingQuantity, eventId, ticketName, price, isActive);
            if (fields != null)
                ticket.EventTicketFields = fields.ToList();

            _eventRepository.CreateEventTicket(ticket);

            return ticket;
        }

        public IEnumerable<EventGuestDetails> BuildGuestList(int? eventId)
        {
            Guard.NotNull(eventId);

            var tickets = _eventRepository.GetEventBookingTicketsForEvent(eventId);

            // Need to fetch all the groups to match each guest to the group
            // We cannot do that at the moment because groups cannot be fetched from EntityFramework (separate stored procedure)
            var groups = _eventRepository.GetEventGroups(eventId.GetValueOrDefault(), eventTicketId: null);

            return tickets.Select(t => new EventGuestDetails
            {
                GuestFullName = t.GuestFullName,
                GuestEmail = t.GuestEmail,
                DynamicFields = t.TicketFieldValues,
                TicketNumber = t.EventBookingTicketId,
                TicketId = t.EventTicketId,
                TicketName = t.TicketName,
                TotalTicketPrice = t.TotalPrice,
                IsPublic = t.IsPublic,
                DateOfBooking = t.CreatedDateTime.GetValueOrDefault(),
                DateOfBookingUtc = t.CreatedDateTimeUtc.GetValueOrDefault(),
                GroupName = groups.SingleOrDefault(g => g.EventGroupId == t.EventGroupId)?.GroupName
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
#if !DEBUG
            if (IsEventEditable(eventId))
            {
                // Only the following details will allowed to be changed if the event has started
                originalEventDetails.Location = location;


                if (locationLatitude.HasValue && originalEventDetails.LocationLatitude != locationLatitude &&
                    locationLongitude.HasValue && originalEventDetails.LocationLongitude != locationLongitude)
                {
                    originalEventDetails.LocationLatitude = locationLatitude;
                    originalEventDetails.LocationLongitude = locationLongitude;

                    // Only perform this in non-debug scenario so that we don't have to waste the timezone look ups for development environments

                    // Update the timezone info using the location service
                    var timezoneResult = _locationService.GetTimezone(locationLatitude.Value, locationLongitude.Value);
                    originalEventDetails.TimeZoneId = timezoneResult.TimeZoneId;
                    originalEventDetails.TimeZoneName = timezoneResult.TimeZoneName;
                    originalEventDetails.TimeZoneDaylightSavingsOffsetSeconds = timezoneResult.DstOffset;
                    originalEventDetails.TimeZoneUtcOffsetSeconds = timezoneResult.RawOffset;


                    // Work out what the UTC date is for these dates which is based on the events location!
                    var totalOffset = timezoneResult.RawOffset + timezoneResult.DstOffset;
                    originalEventDetails.EventStartDate = eventStartDate;
                    originalEventDetails.EventStartDateUtc = eventStartDate.AddSeconds(-totalOffset);
                    originalEventDetails.EventEndDate = eventEndDateTime;
                    originalEventDetails.EventEndDateUtc = eventEndDateTime.AddSeconds(-totalOffset);
                    
                    address.AddressId = originalEventDetails.AddressId;
                    originalEventDetails.Address = address;
                    _eventRepository.UpdateEventAddress(address);
                }
                else
                {
                    _logService.Warn("Unable to update timezone and all dates information. Long and Latitude are missing!");
                }

                onlineAd.Heading = title; // This is used for ticketing so cannot change!
            }
            else
            {
                _logService.Info("Event will be partially updated as it's in a not-modifiable state.");
            }
#endif
            onlineAd.Description = description;
            onlineAd.HtmlText = htmlText;
            onlineAd.ContactName = organiserName;
            onlineAd.ContactPhone = organiserPhone;
            originalEventDetails.LocationFloorPlanDocumentId = floorPlanDocumentId;
            originalEventDetails.LocationFloorPlanFilename = locationFloorPlanFilename;

            _bookingManager.UpdateOnlineAd(adId, onlineAd);
            _eventRepository.UpdateEvent(originalEventDetails);
            _bookingManager.UpdateSchedule(adId, adStartDate);

            _logService.Info("Updated event successfully.");
        }

        public void UpdateEventGroupSettings(int eventId, bool groupsRequired)
        {
            var eventDetails = _eventRepository.GetEventDetails(eventId);
            Guard.NotNull(eventDetails);

            eventDetails.GroupsRequired = groupsRequired;
            _eventRepository.UpdateEvent(eventDetails);
        }

        public void UpdateEventGuestSettings(int eventId, bool displayGuestsToPublic)
        {
            var eventDetails = _eventRepository.GetEventDetails(eventId);
            Guard.NotNull(eventDetails);

            eventDetails.DisplayGuests = displayGuestsToPublic;
            _eventRepository.UpdateEvent(eventDetails);
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

        public IEnumerable<EventGroup> GetEventGroups(int eventId, int? eventTicketId = null)
        {
            return _eventRepository.GetEventGroups(eventId, eventTicketId);
        }

        public async Task<IEnumerable<EventGroup>> GetEventGroupsAsync(int eventId, int? eventTicketId = null)
        {
            return await _eventRepository.GetEventGroupsAsync(eventId, eventTicketId);
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

        public void AddEventGroup(int eventId, string groupName, int? maxGuests, IEnumerable<int> tickets, string createdByUser, bool isDisabled)
        {
            var t = tickets?.ToList() ?? new List<int>();

            // CreateRepository the new event group
            var eventGroup = new EventGroup
            {
                EventId = eventId,
                CreatedDateTime = _dateService.Now,
                CreatedDateTimeUtc = _dateService.UtcNow,
                CreatedBy = createdByUser,
                GroupName = groupName,
                MaxGuests = maxGuests,
                AvailableToAllTickets = t.Count == 0,
                IsDisabled = isDisabled
            };

            _eventRepository.CreateEventGroup(eventGroup, t);
        }

        public void SetEventGroupStatus(int eventGroupId, bool isDisabled)
        {
            _eventRepository.UpdateEventGroupStatus(eventGroupId, isDisabled);
        }

        public void UpdateEventTicketSettings(int eventId, bool includeTransactionFee, DateTime? closingDate, DateTime? openingDate)
        {
            var eventModel = _eventRepository.GetEventDetails(eventId);

            eventModel.IncludeTransactionFee = includeTransactionFee;

            // The dates supplied are assumed to be local to the event.
            // That means we simply take away the daylight savings and Utc offsets ... and we have our Utc dates!
            var totalSecondsToSubstract = eventModel.TimeZoneUtcOffsetSeconds.GetValueOrDefault() +
                                          eventModel.TimeZoneDaylightSavingsOffsetSeconds.GetValueOrDefault();

            eventModel.ClosingDate = closingDate;
            eventModel.ClosingDateUtc = closingDate?.AddSeconds(-totalSecondsToSubstract);

            eventModel.OpeningDate = openingDate;
            eventModel.OpeningDateUtc = openingDate?.AddSeconds(-totalSecondsToSubstract);

            _eventRepository.UpdateEvent(eventModel);
        }

        public EventOrganiser CreateEventOrganiser(int eventId, string username)
        {
            var currentUser = _userManager.GetCurrentUser();

            var eventOrganiser = new EventOrganiser
            {
                EventId = eventId,
                UserId = username,
                IsActive = true,
                LastModifiedDate = _dateService.Now,
                LastModifiedDateUtc = _dateService.UtcNow,
                LastModifiedBy = currentUser.Username
            };

            _eventRepository.CreateEventOrganiser(eventOrganiser);

            return eventOrganiser;
        }
    }
}