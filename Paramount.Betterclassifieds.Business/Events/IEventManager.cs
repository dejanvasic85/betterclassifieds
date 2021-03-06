using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false);
        EventModel GetEventDetails(int eventId);
        EventTicket GetEventTicket(int eventTicketId);
        EventTicket GetEventTicketAndReservations(int eventTicketId);
        Task<IEnumerable<EventTicket>> GetEventTicketsForGroup(int eventGroupId);
        EventBooking GetEventBooking(int eventBookingId);
        EventBookingTicket GetEventBookingTicket(int eventBookingTicketId);
        EventBookingTicket UpdateEventBookingTicket(int eventBookingTicketId, string guestFullName, string guestEmail, int? eventGroupId, bool isPublic, IEnumerable<EventBookingTicketField> fields, Func<string, string> barcodeUrlCreator);
        EventBookingTicket CancelEventBookingTicket(int eventBookingTicketId);

        int GetRemainingTicketCount(int? ticketId);
        int GetRemainingTicketCount(EventTicket eventTicket);
        IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId);
        void ReserveTickets(string sessionId, IEnumerable<EventTicketReservation> reservations);
        TimeSpan GetRemainingTimeForReservationCollection(IEnumerable<EventTicketReservation> reservations);
        IEnumerable<EventBooking> GetEventBookingsForEvent(int eventId);
        EventBooking CreateEventBooking(int eventId, string promoCode, ApplicationUser applicationUser, IEnumerable<EventTicketReservation> currentReservations, Func<string, string> barcodeUrlCreator, string howYouHeardAboutEvent = "");
        void CancelEventBooking(int? eventBookingId);
        void ActivateBooking(int? eventBookingId, long? eventInvitationId);
        void SetPaymentReferenceForBooking(int eventBookingId, string paymentReference, PaymentType paymentType);
        void AdjustRemainingQuantityAndCancelReservations(string sessionId, IList<EventBookingTicket> eventBookingTickets);
        string CreateEventTicketDocument(int eventBookingTicketId, byte[] ticketPdfData);
        void UpdateEventTicket(int eventTicketId, string ticketName, decimal price, int remainingQuantity, string colourCode, string ticketImage, bool isActive, IEnumerable<EventTicketField> fields);
        EventTicket CreateEventTicket(int eventId, string ticketName, decimal price, int availableQty, string colourCode, string ticketImage, bool isActive, IEnumerable<EventTicketField> fields);
        IEnumerable<EventGuestDetails> BuildGuestList(int? eventId);
        EventPaymentSummary BuildPaymentSummary(int? eventId);
        bool AreBookingsPresentForEvent(int? eventId);
        void CreateEventPaymentRequest(int eventId, PaymentType paymentType, decimal requestedAmount);
        EventPaymentRequestStatus GetEventPaymentRequestStatus(int? eventId);
        EventInvitation GetEventInvitation(long eventInvitationId);

        void CloseEvent(int eventId);
        void UpdateEventDetails(int adId, int eventId, string title, string description, string htmlText, DateTime eventStartDate, DateTime eventEndDateTime, string location, decimal? locationLatitude, decimal? locationLongitude, string organiserName, string organiserPhone, DateTime adStartDate, string floorPlanDocumentId, string locationFloorPlanFilename, Address address, string venueName);

        void UpdateEventGroupSettings(int eventId, bool groupsRequired);

        void UpdateEventGuestSettings(int eventId, bool displayGuestsToPublic);

        EventInvitation CreateInvitationForUserNetwork(int eventId, int userNetworkId);
        IEnumerable<EventGroup> GetEventGroups(int eventId, int? eventTicketId = null);
        Task<IEnumerable<EventGroup>> GetEventGroupsAsync(int eventId, int? eventTicketId = null);


        Task<EventGroup> GetEventGroup(int eventGroupId);
        void AssignGroupToTicket(int eventBookingTicketId, int? eventGroupId);

        void AddEventGroup(int eventId, string groupName, int? maxGuests, IEnumerable<int> tickets, string createdByUser, bool isDisabled);
        void SetEventGroupStatus(int eventGroupId, bool isDisabled);
        void UpdateEventTicketSettings(int eventId, bool includeTransactionFee, DateTime? closingDate, DateTime? openingDate);
        void CreateSurveyOption(int eventId, string surveyOption);
        
    }
}