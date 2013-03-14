using System;

namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    public class GetBroadcastActivityRequest : BaseRequest
    {
        public DateTime ReportDate;

        public override string TransactionName
        {
            get { return "GetBroadcastActivityRequest"; }
        }
    }
}