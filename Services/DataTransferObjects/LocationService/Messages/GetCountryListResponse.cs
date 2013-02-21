using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.LocationService.Messages
{
    public class GetCountryListResponse : BaseResponse
    {
        public List<CountryEntity> CountryList { get; set; }
    }
}
