using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventGuestTransferFromNotification : IDocType
    {
        public string DocumentType => "EventGuestTransferFrom";
        public IList<EmailAttachment> Attachments { get; set; }

        public string EventName { get; set; }
        public string EventStartDate { get; set; }
        public string EventUrl { get; set; }
        public string TicketName { get; set; }
        public string NewGuestEmail { get; set; }
        public string NewGuestName { get; set; }
    }
}