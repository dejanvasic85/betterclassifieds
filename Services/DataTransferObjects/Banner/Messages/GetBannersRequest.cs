using System;
using System.Collections.ObjectModel;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetBannersRequest:BaseRequest
    {
        public override string TransactionName
        {
            get { return AuditTransactions.GetBannersRequest; }
        }


        public DateTime EndDate { get; set; }

        public Guid? BannerId { get; set; }

        public DateTime StartDateTime { get; set; }
    }
}