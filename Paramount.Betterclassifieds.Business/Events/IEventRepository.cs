using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventRepository
    {
        // Event
        EventModel GetEventDetails(int eventId);
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false);
        EventTicket GetEventTicketDetails(int ticketId, bool includeReservations = false);
        EventBooking GetEventBooking(int eventBookingId, bool includeTickets = false, bool includeEvent = false);
        EventBookingTicket GetEventBookingTicket(int eventBookingTicketId);
        EventPaymentRequest GetEventPaymentRequestForEvent(int eventId);
        IEnumerable<EventBooking> GetEventBookingsForEvent(int eventId, bool activeOnly = true, bool includeTickets = false);
        IEnumerable<EventTicketReservation> GetEventTicketReservationsForSession(string sessionId);
        IEnumerable<EventTicketReservation> GetEventTicketReservations(int ticketId, bool activeOnly);
        IEnumerable<EventBookingTicket> GetEventBookingTicketsForEvent(int? eventId);
        EventBookingTicketValidation GetEventBookingTicketValidation(int eventBookingTicketId);
        EventInvitation GetEventInvitation(long eventInvitationId);
        Task<IEnumerable<EventGroup>> GetEventGroups(int eventId, int? eventTicketId);
        Task<EventGroup> GetEventGroup(int eventGroupId);
        Task<IEnumerable<EventTicket>> GetEventTickets(int eventId);
        Task<IEnumerable<EventTicket>> GetEventTicketsForGroup(int eventGroupId);

        void CreateEventTicketReservation(EventTicketReservation eventTicketReservation);
        void CreateBooking(EventBooking eventBooking);
        void CreateEventBookingTicket(EventBookingTicket eventBookingTicket);
        void CreateEventPaymentRequest(EventPaymentRequest request);
        void CreateEventTicket(EventTicket ticket);
        void CreateEventBookingTicketValidation(EventBookingTicketValidation eventBookingTicketValidation);
        void CreateEventInvitation(EventInvitation eventInvitation);
        void CreateEventGroup(EventGroup eventGroup, IEnumerable<int> tickets);

        void UpdateEvent(EventModel eventModel);
        void UpdateEventTicketReservation(EventTicketReservation eventTicketReservation);
        void UpdateEventBooking(EventBooking eventBooking);
        void UpdateEventTicket(EventTicket eventTicket);
        void UpdateEventAddress(Address address);
        void UpdateEventBookingTicketValidation(EventBookingTicketValidation eventBookingTicketValidation);
        void UpdateEventInvitation(EventInvitation invitation);
        void UpdateEventBookingTicket(EventBookingTicket eventBookingTicket);
        void UpdateEventBookingTicketField(EventBookingTicketField eventBookingTicketField);
        void UpdateEventGroupStatus(int eventGroupId, bool isDisabled);
    }
}