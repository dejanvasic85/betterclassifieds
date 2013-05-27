using System;
using Paramount.Common.DataTransferObjects.LoggingService;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;

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
