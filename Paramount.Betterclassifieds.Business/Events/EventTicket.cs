using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    /// <summary>
    /// Ticket definition for an event
    /// </summary>
    public class EventTicket
    {
        public EventTicket()
        {
            EventTicketReservations = new List<EventTicketReservation>();
            EventBookingTickets = new List<EventBookingTicket>();
            EventTicketFields = new List<EventTicketField>();
        }

        public int? EventTicketId { get; set; }
        public int? EventId { get; set; }
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string ColourCode { get; set; }
        public IList<EventTicketReservation> EventTicketReservations { get; set; }
        public IList<EventBookingTicket> EventBookingTickets { get; set; }
        public IList<EventTicketField> EventTicketFields { get; set; }
        public IList<EventSeatBooking> EventSeats { get; set; }
    }
}