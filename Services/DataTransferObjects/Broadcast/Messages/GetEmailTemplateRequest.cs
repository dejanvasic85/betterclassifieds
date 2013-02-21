using System;

namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetEmailTemplateRequest : BaseRequest
    {
        [DataMember]
        public string TemplateName { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetEmailTemplate; }
        }
    }
}
