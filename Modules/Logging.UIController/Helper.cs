using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;
using Paramount.Common.DataTransferObjects.LoggingService;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Common.DataTransferObjects;
using System.Web;
using Paramount.Services.Proxy;
using System.Configuration;

namespace Paramount.Modules.Logging.UIController
{
    internal static class Helper
    {
        public static LogAddRequest MakeLogRequest(string data1, string data2, CategoryType categoryType)
        {
            // Construct the request to call the Log Service
            LogAddRequest request = new LogAddRequest();

            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.Domain = ConfigSettingReader.Domain;
            request.Initialise = true;

            var browser = HttpContext.Current.Request.Browser;
            var browserInfoFormatted = string.Format("{0} {1}", browser.Browser, browser.Version);
            var sessionId = HttpContext.Current.Session != null ? HttpContext.Current.Session.SessionID : null;
            request.AuditData = new AuditData()
            {
                AccountId = ConfigSettingReader.ClientCode,
                BrowserType = string.Format("Browser = [{0}] UserAgent = [{1}]", browserInfoFormatted, HttpContext.Current.Request.UserAgent),
                ClientIpAddress = HttpContext.Current.Request.UserHostAddress,
                HostName = HttpContext.Current.Request.UserHostName,
                SessionId = sessionId,
                Username = HttpContext.Current.User.Identity.Name,
                CreatedDate = DateTime.Now,
                CategoryType = categoryType,
                Data1 = data1,
                Data2 = data2,
            };

            return request;
        }

        public static void SendLogRequest(LogAddRequest logAddRequest)
        {
            try
            {
                WebServiceHostManager.LogServiceClient.Add(logAddRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
