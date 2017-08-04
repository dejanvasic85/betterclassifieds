namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventSeat
    {
        public long EventSeatId { get; set; }
        public int? EventTicketId { get; set; }
        public int SeatOrder { get; set; }
        public string SeatNumber { get; set; }
        public bool? NotAvailableToPublic { get; set; }
        public EventTicket EventTicket { get; set; }
        public string RowNumber { get; set; }
        public int RowOrder { get; set; }
        public bool IsBooked { get; set; }
    }
}