using System;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBooking
    {
        public int EventBookingId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public decimal TotalCost { get; set; }
        public PaymentType PaymentType { get; set; }
        public EventBookingStatus Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
    }
}