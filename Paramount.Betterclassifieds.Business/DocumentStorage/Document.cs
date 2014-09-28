using System;

namespace Paramount.Betterclassifieds.Business
{
    public class Document
    {
        public Document()
        { }

        public Document(Guid documentId, byte[] data, string contentType, string fileName, int fileLength, string user)
        {
            this.DocumentId = documentId;
            this.Data = data;
            this.ContentType = contentType;
            this.FileName = fileName;
            this.FileLength = fileLength;
            this.Username = user;

            // Set the legacy obsolete just for now.
            this.ApplicationCode = "Betterclassifieds";
            this.EntityCode = "P000000005";
            this.DocumentCategoryId = 5;

            this.CreatedDate = DateTime.Now; // Todo also create utc columns
            this.UpdatedDate = DateTime.Now;
        }

        public Guid DocumentId { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string Username { get; set; }
        public string FileName { get; set; }
        public int FileLength { get; set; }
        public string Reference { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Obsolete]
        public string ApplicationCode { get; private set; }

        [Obsolete]
        public string EntityCode { get; private set; }

        [Obsolete]
        public int DocumentCategoryId { get; private set; }
    }
}