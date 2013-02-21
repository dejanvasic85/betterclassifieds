namespace Paramount.ApplicationBlock.Logging
{
    using AuditLogging;
    using Interface;
    public static class Logger
    {
        public static void Log(ILog log)
        {
            if(log as AuditLog != null)
            {
                AuditLogManager.Log((AuditLog)log);
            }
        }

        public static string AuditLogType
        {
            get
            {
                return typeof (AuditLog).FullName;
            }
        }
    }
}
