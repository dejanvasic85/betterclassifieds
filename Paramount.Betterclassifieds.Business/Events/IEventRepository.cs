﻿using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventRepository
    {
        // Event
        EventModel GetEventDetails(int eventId);
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false);
        EventTicket GetEventTicketDetails(int ticketId, bool includeReservations = false);
        EventBooking GetEventBooking(int eventBookingId, bool includeTickets = false);
        IEnumerable<EventTicketReservation> GetEventTicketReservationsForSession(string sessionId);
        IEnumerable<EventTicketReservation> GetEventTicketReservations(int ticketId, bool activeOnly);
        IEnumerable<EventBookingTicket> GetEventBookingTicketsForEvent(int? eventId);

        void CreateEventTicketReservation(EventTicketReservation eventTicketReservation);
        void CreateBooking(EventBooking eventBooking);

        void UpdateEventTicketReservation(EventTicketReservation eventTicketReservation);
        void UpdateEventBooking(EventBooking eventBooking);
        void UpdateEventTicket(EventTicket eventTicket);
        void CreateEventTicket(EventTicket ticket);
        
    }
}