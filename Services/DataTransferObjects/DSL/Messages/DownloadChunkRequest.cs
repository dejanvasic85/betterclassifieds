namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DownloadChunkRequest : BaseRequest
    {
        [DataMember]
        public Guid DocumentId { get; set; }

        [DataMember(IsRequired = true)]
        public int Offset { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.DownloadChunk; }
        }
    }
}
