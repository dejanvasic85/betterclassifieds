using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.LocationService.Messages
{
    public class GetStateListResponse : BaseResponse
    {
        public List<StateEntity> StateList { get; set; }

        
    }
}
