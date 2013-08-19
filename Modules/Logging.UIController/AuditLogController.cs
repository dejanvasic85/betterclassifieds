using Paramount.Common.DataTransferObjects.LoggingService;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;
using Paramount.Utility;

namespace Paramount.Modules.Logging.UIController
{
    public static class AuditLogController<T> where T : class
    {
        public static void AuditWebTransaction(T dataObject, string bamPromotedProperties)
        {
            // Convert the dataObject to String XML
            string data1 = XmlUtilities.SerialiseObjectPureXml(dataObject);
            LogAddRequest logAddRequest = Helper.MakeLogRequest(data1, bamPromotedProperties, CategoryType.AuditLog);
            Helper.SendLogRequest(logAddRequest);
        }
    }
}
