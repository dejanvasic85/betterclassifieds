using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.LocationService.Messages
{
    public class GetCountryListRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "LocationService.GetCountryListRequest"; }
        }

        #endregion
    }
}
