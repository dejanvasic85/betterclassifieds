using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class ExpirationReminder : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; set; }


        [Placeholder("AdReference")]
        public string AdReference { get; set; }

        [Placeholder("LinkForExtension")]
        public string LinkForExtension { get; set; }
    }
}