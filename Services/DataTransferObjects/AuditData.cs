using System;
using Paramount.Common.DataTransferObjects.LoggingService;

namespace Paramount.Common.DataTransferObjects
{
    public class AuditData
    {
        public string Username { get; set; }
        public string ClientIpAddress { get; set;}
        public string BrowserType { get; set; }
        public string SessionId { get; set; }
        public string GroupingId { get; set; }
        public string HostName { get; set; }
        public CategoryType CategoryType { get; set; }
        public string AccountId { get; set; }        
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}