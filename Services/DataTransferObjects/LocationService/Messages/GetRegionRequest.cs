using System;

namespace Paramount.Common.DataTransferObjects.LocationService.Messages
{
    public class GetRegionRequest : BaseRequest
    {
        public string StateCode { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
