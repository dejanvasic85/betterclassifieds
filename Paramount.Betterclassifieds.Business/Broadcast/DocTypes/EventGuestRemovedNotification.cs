using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventGuestRemovedNotification : IDocType
    {
        public EventGuestRemovedNotification()
        {
            Attachments = new List<EmailAttachment>();
        }
        public string DocumentType => "EventGuestRemoved";
        public IList<EmailAttachment> Attachments { get; set; }

        [Placeholder("EventBookingTicketId")]
        public int EventBookingTicketId { get; set; }
        [Placeholder("EventName")]
        public string EventName { get; set; }
        [Placeholder("EventDate")]
        public string EventDate { get; set; }
        [Placeholder("EventUrl")]
        public string EventUrl { get; set; }
    }
}