using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventPaymentRequest : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; set; }

        [Placeholder("PreferredPaymentMethod")]
        public string PreferredPaymentMethod { get; set; }
        [Placeholder("RequestedAmount")]
        public decimal? RequestedAmount { get; set; }
        [Placeholder("EventId")]
        public int EventId { get; set; }
        [Placeholder("AdId")]
        public int AdId { get; set; }
        [Placeholder("Username")]
        public string Username { get; set; }
    }
}