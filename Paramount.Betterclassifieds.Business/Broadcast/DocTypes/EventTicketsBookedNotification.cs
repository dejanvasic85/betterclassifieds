using System;
using System.Net.Mime;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventTicketsBookedNotification : IDocType
    {
        public string DocumentType { get { return "EventTicketsBooked"; } }
        public EmailAttachment[] Attachments { get; private set; }

        public EventTicketsBookedNotification WithTickets(byte[] ticketsPdfFileContent)
        {
            Attachments = new[]
            {
                new EmailAttachment{
                    Content =  ticketsPdfFileContent, 
                    ContentType = MediaTypeNames.Application.Pdf, 
                    FileName = "Tickets.pdf"}, 
            };
            return this;
        }

        [Placeholder("EventName")]
        public string EventName { get; set; }
        [Placeholder("StartDateTime")]
        public DateTime StartDateTime { get; set; }
        [Placeholder("EndDateTime")]
        public DateTime EndDateTime { get; set; }
        [Placeholder("OrganiserName")]
        public string OrganiserName { get; set; }
        [Placeholder("OrganiserEmail")]
        public string OrganiserEmail { get; set; }
        [Placeholder("Address")]
        public string Address { get; set; }
        [Placeholder("LocationLatitude")]
        public decimal? LocationLatitude { get; set; }
        [Placeholder("LocationLongitude")]
        public decimal? LocationLongitude { get; set; }
        [Placeholder("CustomerFirstName")]
        public string CustomerFirstName { get; set; }
        [Placeholder("CustomerLastName")]
        public string CustomerLastName { get; set; }
        [Placeholder("CustomerEmailAddress")]
        public string CustomerEmailAddress { get; set; }
        [Placeholder("EventUrl")]
        public string EventUrl { get; set; }

    }
}