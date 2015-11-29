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

        /// <summary>
        /// Crawls through exceptions until it reaches the most inner exception and returns it
        /// </summary>
        public static Exception InnerMostException(this Exception exception)
        {
            return GetInner(exception);
        }

        private static Exception GetInner(Exception exception)
        {
            if (exception == null)
                return null;

            if (exception.InnerException == null)
                return exception;

            return GetInner(exception.InnerException);
        }
    }
}