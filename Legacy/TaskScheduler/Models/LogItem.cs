using System;

namespace Paramount.TaskScheduler.Models
{
    public class LogItem
    {
        public string Application { get; set; }
        public string Host { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public string StatusCode { get; set; }
        public DateTime TimeUtc { get; set; }
    }
}
