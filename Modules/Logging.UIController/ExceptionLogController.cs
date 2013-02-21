using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;
using Paramount.Common.DataTransferObjects.LoggingService;

namespace Paramount.Modules.Logging.UIController
{
    public class ExceptionLogController<T> where T : Exception
    {
        public static void AuditException(T exception)
        {
            LogAddRequest logAddRequest = Helper.MakeLogRequest(exception.Message, exception.StackTrace, CategoryType.EventLog);
            Helper.SendLogRequest(logAddRequest);
        }
    }
}
