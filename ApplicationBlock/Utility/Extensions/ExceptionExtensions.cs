using System;
using System.Diagnostics;

namespace Paramount
{
    /// <summary>
    /// Exception extension methods
    /// </summary>
    //[DebuggerStepThrough()]
    public static class ExceptionExtentions
    {
        /// <summary>
        /// Writes the event details to the Windows Event log Viewer.
        /// </summary>
        /// <param name="exception">Exception to be logged to the Event Viewer</param>
        /// <param name="source">The source name. Default value is "Paramount"</param>
        /// <param name="logName">The log category. E.g. Application, System or Custom. Default value is Application</param>
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