namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class UploadFileRequest : BaseRequest
    {
        [DataMember]
        public Guid DocumentId { get; set; }

        [DataMember]
        public int CategoryCode;

        [DataMember]
        public string ApplicationCode { get; set; }

        [DataMember]
        public string AccountId { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileType { get; set; }

        [DataMember]
        public int FileLength { get; set; }

        [DataMember]
        public string ReferenceData { get; set; }

        [DataMember]
        public bool IsPrivate { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public string DocumentCategoryId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.UploadFile; }
        }
    }
}
