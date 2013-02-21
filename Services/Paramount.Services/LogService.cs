using System;
using System.ServiceModel;
using Paramount.Common.DataTransferObjects.LoggingService;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;
using Paramount.Common.ServiceContracts;
using Paramount.DataService;

namespace Paramount.Services
{
    public class LogService : ILogService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public LogAddResponse Add(LogAddRequest request)
        {
            LogAddResponse response = new LogAddResponse();
            try
            {
                // Log the request by simply passing the details down to the AuditLogManager
                string logId = LoggingDataService.CreateAuditLog(null, request.ApplicationName,
                    request.Domain, request.RequestTransactionName, request.AuditData.CategoryType.ToString(),
                    request.AuditData.Username, request.AuditData.AccountId,
                    request.AuditData.Data1, request.AuditData.Data2, request.AuditData.CreatedDate,
                    request.AuditData.SessionId, request.AuditData.ClientIpAddress, request.AuditData.HostName,
                    request.AuditData.BrowserType);

                if (string.IsNullOrEmpty(logId))
                    throw new ApplicationException("Failed to add log entry");
            }
            catch (Exception ex)
            {
                // Cleanse the exception

                throw new FaultException<ExceptionDetail>(new ExceptionDetail(ex), ex.Message, FaultCode.CreateReceiverFaultCode(new FaultCode("Log")));
            }
            return response;
        }
    }
}
