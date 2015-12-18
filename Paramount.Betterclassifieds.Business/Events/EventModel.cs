using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventModel : ICategoryAd
    {
        public EventModel()
        {
            Tickets = new List<EventTicket>();
            EventBookings = new List<EventBooking>();
        }

        public int? EventId { get; set; }
        public int OnlineAdId { get; set; }
        public string Location { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public IList<EventTicket> Tickets { get; set; }
        public IList<EventTicketField> TicketFields { get; set; }
        public IList<EventBooking> EventBookings { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? ClosingDateUtc { get; set; }

        public bool IsClosed
        {
            get { return this.ClosingDateUtc.HasValue && this.ClosingDateUtc <= DateTime.UtcNow; }
        }
    }
}
