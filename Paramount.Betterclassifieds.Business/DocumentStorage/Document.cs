using System;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}