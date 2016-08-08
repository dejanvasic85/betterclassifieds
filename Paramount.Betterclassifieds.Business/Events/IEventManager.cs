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
        Task<IEnumerable<int>> GetEventTicketsForGroup(int eventGroupId);
        EventBooking GetEventBooking(int eventBookingId);
        int GetRemainingTicketCount(int? ticketId);
        int GetRemainingTicketCount(EventTicket eventTicket);
        IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId);
        void ReserveTickets(string sessionId, IEnumerable<EventTicketReservation> reservations);
        TimeSpan GetRemainingTimeForReservationCollection(IEnumerable<EventTicketReservation> reservations);
        EventBooking CreateEventBooking(int eventId, ApplicationUser applicationUser, IEnumerable<EventTicketReservation> currentReservations);
        void CancelEventBooking(int? eventBookingId);
        void ActivateBooking(int? eventBookingId, long? eventInvitationId);
        void SetPaymentReferenceForBooking(int eventBookingId, string paymentReference, PaymentType paymentType);
        void AdjustRemainingQuantityAndCancelReservations(string sessionId, IList<EventBookingTicket> eventBookingTickets);
        string CreateEventTicketsDocument(int eventBookingId, byte[] ticketPdfData, DateTime? ticketsSentDate = null);
        void UpdateEventTicket(int eventTicketId, string ticketName, decimal price, int remainingQuantity);
        void CreateEventTicket(int eventId, string ticketName, decimal price, int remainingQuantity, IEnumerable<EventTicketField> fields);
        IEnumerable<EventGuestDetails> BuildGuestList(int? eventId);
        EventPaymentSummary BuildPaymentSummary(int? eventId);
        bool IsEventEditable(int? eventId);
        void CreateEventPaymentRequest(int eventId, PaymentType paymentType, decimal requestedAmount, string requestedByUser);
        EventPaymentRequestStatus GetEventPaymentRequestStatus(int? eventId);
        EventInvitation GetEventInvitation(long eventInvitationId);

        void CloseEvent(int eventId);
        void UpdateEventDetails(int adId, int eventId, string title, string description, string htmlText,
            DateTime eventStartDate, DateTime eventEndDateTime, string location,
            decimal? locationLatitude, decimal? locationLongitude, string organiserName,
            string organiserPhone, DateTime adStartDate, string floorPlanDocumentId, string locationFloorPlanFilename,
            Address address);

        EventInvitation CreateInvitationForUserNetwork(int eventId, int userNetworkId);

        Task<IEnumerable<EventGroup>> GetEventGroups(int eventId, int? eventTicketId = null);

        Task<EventGroup> GetEventGroup(int eventGroupId);
        void AssignGroupToTicket(int eventBookingTicketId, int? eventGroupId);

        void AddEventGroup(int eventId, string groupName, int? maxGuests, IEnumerable<int> tickets, string createdByUser, bool isDisabled);
        void SetEventGroupStatus(int eventGroupId, bool isDisabled);
        void SetTransactionFee(int eventId, bool includeTransactionFee);
    }
}