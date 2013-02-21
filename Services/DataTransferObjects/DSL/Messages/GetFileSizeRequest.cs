namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.Runtime.Serialization;
    using System;

    [DataContract]
    public class GetFileSizeRequest : BaseRequest
    {
        [DataMember]
        public Guid DocumentId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetFileSize; }
        }
    }
}
