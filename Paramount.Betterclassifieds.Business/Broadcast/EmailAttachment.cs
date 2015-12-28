using System;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailAttachment
    {
        public long EmailAttachmentId { get; set; }
        public long? EmailDeliveryId { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public Email Email { get; set; }
    }
}
