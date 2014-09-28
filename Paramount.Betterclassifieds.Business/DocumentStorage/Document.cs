using System;

namespace Paramount.Betterclassifieds.Business
{
    public class Document
    {
        public Document()
        {
            
        }

        public Document(Guid documentId, byte[] data, string contentType)
        {
            this.DocumentId = documentId;
            this.Data = data;
            this.ContentType = contentType;
        }

        public Guid DocumentId { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}