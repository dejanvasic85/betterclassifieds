using System;

namespace Paramount.Betterclassifieds.Business
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}