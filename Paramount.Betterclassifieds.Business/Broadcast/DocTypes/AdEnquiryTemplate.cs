using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class AdEnquiryTemplate : IDocType
    {
        public string DocumentType
        {
            get { return "AdEnquiry"; }
        }

        public IList<EmailAttachment> Attachments { get; }


        [Placeholder("adnumber")]
        public string AdNumber { get; set; }
        [Placeholder("name")]
        public string Name { get; set; }
        [Placeholder("email")]
        public string Email { get; set; }

        [Placeholder("question")]
        public string Question { get; set; }
    }
}