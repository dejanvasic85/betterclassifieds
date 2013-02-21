namespace Paramount.ApplicationBlock.Logging.EventLogging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Logging.Interface;
    using DataAccess;

    public static class EventLogManager
    {
        public static void Log(EventLog log)
        {
            LoggingDataService.CreateLog(
                null,
                log.Application,
                log.Domain,
                log.TransactionName,
                log.Category.ToString(),
                log.User,
                log.AccountId,
                log.Data,
                log.SecondaryData,
                log.DateTimeCreated,
                log.SessionId,
                log.IPAddress,
                log.HostName);
        }

        public static void Log(Exception exception)
        {
            Log( new EventLog(exception));
        }
    }
}
