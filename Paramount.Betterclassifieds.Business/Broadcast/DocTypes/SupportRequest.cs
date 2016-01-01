using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class SupportRequest : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; }


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