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
        //public DateTime? ReservationExpiryUtc { get; set; }
        

        //private bool IsReserved()
        //{
        //    return ReservationExpiryUtc.HasValue;
        //}

        //public bool IsAvailable()
        //{
        //    return !IsReserved() && IsAvailableToPublic() && !IsBooked();
        //}

        //private bool IsAvailableToPublic()
        //{
        //    return !NotAvailableToPublic.GetValueOrDefault();
        //}

        //private bool IsBooked()
        //{
        //    return EventBookingTicketId.HasValue;
        //}
    }
}