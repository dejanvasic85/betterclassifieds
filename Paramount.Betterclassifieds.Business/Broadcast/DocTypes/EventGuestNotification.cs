using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventGuestNotification : IDocType
    {
        public EventGuestNotification()
        {
            Attachments = new List<EmailAttachment>();
        }
        public string DocumentType => "EventGuest";
        public IList<EmailAttachment> Attachments { get; set; }


        [Placeholder("EventName")]
        public string EventName { get; set; }

        [Placeholder("EventUrl")]
        public string EventUrl { get; set; }

        [Placeholder("PurchaserName")]
        public string PurchaserName { get; set; }

        [Placeholder("ClientName")]
        public string ClientName { get; set; } // e.g. KandoBay

        [Placeholder("Location")]
        public string Location { get; set; }

        [Placeholder("EventStartDate")]
        public string EventStartDate { get; set; }

        [Placeholder("EventEndDate")]
        public string EventEndDate { get; set; }

        [Placeholder("TicketType")]
        public string TicketType { get; set; }

        [Placeholder("BarcodeImageData")]
        public string  BarcodeImageData { get; set; }

        public EventGuestNotification WithCalendarInvite(byte[] calendarContent)
        {
            Attachments.Add(
                new EmailAttachment
                {
                    ContentType = ContentType.Calendar,
                    FileName = "Event_Invite.ics",
                    Content = calendarContent
                });
            return this;
        }
    }
}