using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventTicketsBookedNotification : IDocType
    {
        public EventTicketsBookedNotification()
        {
            this.Attachments = new List<EmailAttachment>();
        }

        public string DocumentType => "EventTicketsBooked";
        public IList<EmailAttachment> Attachments { get; set; }

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
        
        public byte[] TicketPdfData { get; private set; }
        
        public EventTicketsBookedNotification WithInvoice(byte[] invoicePdf)
        {
            Attachments.Add(
                new EmailAttachment
                {
                    Content = invoicePdf,
                    ContentType = MediaTypeNames.Application.Pdf,
                    FileName = "Invoice.pdf"
                });
            return this;
        }
    }
}