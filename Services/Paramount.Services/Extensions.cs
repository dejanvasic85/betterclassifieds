namespace Paramount.Services
{
    using System;
    using ApplicationBlock.Logging.AuditLogging;
    using Common.DataTransferObjects;
    using Utility;

    public static class Extensions
    {
        public static AuditLog ConvertToAudit( this BaseRequest request, object data)
        {
            var auditLog = new AuditLog
            {
                AccountId = request.ClientCode,
                SecondaryData = request.AuditData.GroupingId,
                Data = XmlUtilities.SerializeObject(data),
                TransactionName = request.TransactionName,
                IPAddress = request.AuditData.ClientIpAddress,
                SessionId = request.AuditData.SessionId,
                User = request.AuditData.Username,
                Application = request.ApplicationName,
                DateTimeCreated = DateTime.Now,
                Domain = request.Domain,
                HostName = request.AuditData.HostName
            };

            return auditLog;
        }

        public static AuditLog ConvertToAudit( this BaseRequest request,string transactionName , object data)
        {
            var auditLog = new AuditLog
            {
                AccountId = request.ClientCode,
                SecondaryData = request.AuditData.GroupingId,
                Data = XmlUtilities.SerializeObject(data),
                TransactionName =  transactionName,
                IPAddress = request.AuditData.ClientIpAddress,
                SessionId = request.AuditData.SessionId,
                User = request.AuditData.Username,
                Application = request.ApplicationName,
                DateTimeCreated = DateTime.Now,
                Domain = request.Domain,
                HostName = request.AuditData.HostName
            };

            return auditLog;
        }
    }
}