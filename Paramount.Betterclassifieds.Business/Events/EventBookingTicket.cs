using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicket
    {
        public EventBookingTicket()
        {
            TicketFieldValues = new List<EventBookingTicketField>();
        }
        public int EventBookingTicketId { get; set; }
        public int EventBookingId { get; set; }
        public EventBooking EventBooking { get; set; }
        public int EventTicketId { get; set; }
        public EventTicket EventTicket { get; set; }
        public string TicketName { get; set; }
        public decimal? Price { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public List<EventBookingTicketField> TicketFieldValues { get; set; }
        public decimal? TransactionFee { get; set; }
        public decimal TotalPrice { get; set; }
        public int? EventGroupId { get; set; }
        public EventGroup EventGroup { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastModifiedDateUtc { get; set; }
        public string LastModifiedBy { get; set; }
    }
}