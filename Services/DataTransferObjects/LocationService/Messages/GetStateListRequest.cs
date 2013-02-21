using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.LocationService.Messages
{
    public class GetStateListRequest : BaseRequest
    {
        public string Country { get; set; }

        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "LocationService.GetStateList"; }
        }

        #endregion
    }
}
