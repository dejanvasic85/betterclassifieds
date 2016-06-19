using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class SupportRequest : IDocType
    {
        public string DocumentType => GetType().Name;
        public IList<EmailAttachment> Attachments { get; set; }


        [Placeholder("content")]
        public string RequestDetails { get; set; }

        [Placeholder("name")]
        public string Name { get; set; }

        [Placeholder("email")]
        public string Email { get; set; }

        [Placeholder("phone")]
        public string Phone { get; set; }
    }
}