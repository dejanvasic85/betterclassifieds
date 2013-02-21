using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;
using System.ServiceModel;

namespace Paramount.Common.ServiceContracts
{
    [ServiceContract]
    public interface ILogService
    {
        [OperationContract]
        LogAddResponse Add(LogAddRequest request);
    }
}
