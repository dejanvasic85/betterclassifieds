using System;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetNextBannerAdRequest:BaseRequest
    {
        public Collection<CodeDescription> Parameters { get; set; }

        public Guid GroupId { get; set; }

        public GetNextBannerAdRequest()
        {
            Parameters = new Collection<CodeDescription>();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.GetNextBannerAd; }
        }
    }
}
