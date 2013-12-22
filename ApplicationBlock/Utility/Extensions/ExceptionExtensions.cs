namespace Paramount
{
    using System;
    using System.Diagnostics;

    public static class ExceptionExtentions
    {
        public static void ToEventLog(this Exception exception, string source = "Paramount", string logName = "Application")
        {
            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, logName);

            EventLog.WriteEntry(
                source,
                string.Format("Unexpected event has been encountered.\nMessage : {0} \nStacktrace : {1}", exception.Message, exception.StackTrace),
                EventLogEntryType.Error,
                100);
        }
    }
}