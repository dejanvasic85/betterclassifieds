namespace Paramount.Common.DataTransferObjects.DSL
{
    using System;
    using System.IO;

    [Serializable]
    public class DslDocument
    {
        public Guid DocumentId { get; set; }
        public string ApplicationCode { get; set; }
        public string EntityCode { get; set; }
        public Guid? AccounId { get; set; }
        public DslDocumentCategoryType DocumentCategory { get; set; }
        public string Username { get; set; }
        public string FileType { get; set; }
        public Stream FileData { get; set; }
        public int FileLength { get; set; }
        public decimal NumberOfChunks { get; set; }
        public string FileName { get; set; }
        public string Reference { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
