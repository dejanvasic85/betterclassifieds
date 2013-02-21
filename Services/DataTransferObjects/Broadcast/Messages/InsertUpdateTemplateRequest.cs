using System;

namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class InsertUpdateTemplateRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public bool Update { get; set; }


        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string EmailContent { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Sender { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.InsertUpdateTemplate; }
        }
    }
}