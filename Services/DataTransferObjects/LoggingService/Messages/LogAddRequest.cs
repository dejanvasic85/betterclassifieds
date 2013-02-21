using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Paramount.Common.DataTransferObjects.LoggingService.Messages
{
    [DataContract()]
    public class LogAddRequest : BaseRequest
    {
        public LogAddRequest()
        {

        }

        public override string TransactionName
        {
            get
            {
                return "AddLog";
            }
        }
    }
}
