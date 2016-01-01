
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class AdShare : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; }


        [Placeholder("AdvertiserName")]
        public string AdvertiserName { get; set; }

        [Placeholder("AdDescription")]
        public string AdDescription { get; set; }

        [Placeholder("ClientName")]
        public string ClientName { get; set; }

        [Placeholder("AdTitle")]
        public string AdTitle { get; set; }


    }
}
