using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicket
    {
        public EventTicket()
        {
            EventTicketReservations = new List<EventTicketReservation>();
        }
        public int? TicketId { get; set; }
        public int? EventId { get; set; }
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public IList<EventTicketReservation> EventTicketReservations { get; set; }
        //public IList<EventTicketBooking> EventTicketBookings { get; set; }
    }
}