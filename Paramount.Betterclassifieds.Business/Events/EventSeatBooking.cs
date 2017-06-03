﻿using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventSeatBooking
    {
        public long EventSeatBookingId { get; set; }
        public int? EventTicketId { get; set; }
        public int? EventBookingTicketId { get; set; }

        public int SeatOrder { get; set; }
        public string SeatNumber { get; set; }
        public bool? NotAvailableToPublic { get; set; }
        public EventTicket EventTicket { get; set; }
        public string RowNumber { get; set; }
        public int RowOrder { get; set; }
        public DateTime? ReservationExpiryUtc { get; set; }
        

        public bool IsReserved()
        {
            return ReservationExpiryUtc.HasValue;
        }

        public bool IsAvailable()
        {
            return !IsReserved() && IsAvailableToPublic() && !IsBooked();
        }

        public bool IsAvailableToPublic()
        {
            return !NotAvailableToPublic.GetValueOrDefault();
        }

        public bool IsBooked()
        {
            return EventBookingTicketId.HasValue;
        }
    }
}