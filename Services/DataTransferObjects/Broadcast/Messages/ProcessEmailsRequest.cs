namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class ProcessEmailsRequest : BaseRequest
    {
        [DataMember]
        public Guid? BroadcastId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.ProcessEmails; }
        }
    }
}