using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservation
    {
        public int? EventTicketReservationId { get; set; }
        public int? EventTicketId { get; set; }
        public EventTicket EventTicket { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string SessionId { get; set; }
        public EventTicketReservationStatus Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }

        // Only because of entity framework
        public virtual string StatusAsString
        {
            get { return Status.ToString(); }
            set
            {
                EventTicketReservationStatus status;
                if (Enum.TryParse(value, out status))
                {
                    this.Status = status;
                }
            }
        }
    }
}