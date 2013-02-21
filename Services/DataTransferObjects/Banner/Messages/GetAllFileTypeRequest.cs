using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetAllFileTypeRequest : BaseRequest
    {
        public override string TransactionName
        {
            get { return AuditTransactions.GetAllFileType; }
        }
    }
}