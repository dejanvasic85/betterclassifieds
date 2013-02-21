using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Paramount.Common.DataTransferObjects.LoggingService.Messages
{
    [DataContract()]
    public class LogAddResponse
    {
        [DataMember(Name = "LogId", IsRequired = false)]
        public string LogId;
    }
}
