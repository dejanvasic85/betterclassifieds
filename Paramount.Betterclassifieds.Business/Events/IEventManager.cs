using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false);
        EventModel GetEventDetails(int eventId);
        EventBooking GetEventBooking(int eventBookingId);
        int GetRemainingTicketCount(int? ticketId);
        int GetRemainingTicketCount(EventTicket eventTicket);
        IEnumerable<EventTicketReservation> GetTicketReservations(string sessionId);
        void ReserveTickets(string sessionId, IEnumerable<EventTicketReservation> reservations);
        TimeSpan GetRemainingTimeForReservationCollection(IEnumerable<EventTicketReservation> reservations);
        EventBooking CreateEventBooking(int eventId, ApplicationUser applicationUser, IEnumerable<EventTicketReservation> currentReservations);
        void CancelEventBooking(int? eventBookingId);
        void EventBookingPaymentCompleted(int? eventBookingId, PaymentType paymentType);
        void SetPaymentReferenceForBooking(int eventBookingId, string paymentReference, PaymentType paymentType);
        void AdjustRemainingQuantityAndCancelReservations(string sessionId, IList<EventBookingTicket> eventBookingTickets);
        string CreateEventTicketsDocument(int eventBookingId, byte[] ticketPdfData, DateTime? ticketsSentDate = null);
        void UpdateEventTicket(int eventTicketId, string ticketName, decimal price, int remainingQuantity);
        void CreateEventTicket(int eventId, string ticketName, decimal price, int remainingQuantity);
        IEnumerable<EventGuestDetails> BuildGuestList(int? eventId);
        EventPaymentSummary BuildPaymentSummary(int? eventId);
        bool IsEventEditable(int? eventId);
        void CreateEventPaymentRequest(int eventId, PaymentType paymentType, decimal requestedAmount, string requestedByUser);
        EventPaymentRequestStatus GetEventPaymentRequestStatus(int? eventId);
        void CloseEvent(int eventId);
        void UpdateEventDetails(int adId, int eventId, string title, string description, string htmlText, 
            DateTime eventStartDate, DateTime eventEndDateTime, string location, 
            decimal? locationLatitude, decimal? locationLongitude, string organiserName, 
            string organiserPhone, DateTime adStartDate, string floorPlanDocumentId, string locationFloorPlanFilename,
            Address address );
    }
}