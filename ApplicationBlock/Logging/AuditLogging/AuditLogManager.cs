namespace Paramount.ApplicationBlock.Logging.AuditLogging
{
    using DataAccess;

    public static class AuditLogManager
    {
        public static void Log(AuditLog log)
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
    }
}
