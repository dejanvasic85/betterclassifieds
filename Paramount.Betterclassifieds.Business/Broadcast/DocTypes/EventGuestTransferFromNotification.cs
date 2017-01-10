using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventGuestTransferFromNotification : IDocType
    {
        public string DocumentType => "EventGuestTransferFrom";
        public IList<EmailAttachment> Attachments { get; set; }

        [Placeholder("EventName")]
        public string EventName { get; set; }

        [Placeholder("EventStartDate")]
        public string EventStartDate { get; set; }

        [Placeholder("EventUrl")]
        public string EventUrl { get; set; }

        [Placeholder("TicketName")]
        public string TicketName { get; set; }

        [Placeholder("NewGuestEmail")]
        public string NewGuestEmail { get; set; }

        [Placeholder("NewGuestName")]
        public string NewGuestName { get; set; }
    }
}