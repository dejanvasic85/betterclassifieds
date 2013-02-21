using System;

namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetEmailTemplateListRequest : BaseRequest
    {
        public override string TransactionName
        {
            get { return AuditTransactions.GetEmailTemplateList; }
        }
    }
}