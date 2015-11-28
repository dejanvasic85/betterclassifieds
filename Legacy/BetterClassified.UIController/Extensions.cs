using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.DataTransferObjects;
using Paramount.ApplicationBlock.Configuration;
using System.Web;

namespace BetterClassified.UIController
{
    public static class Extensions
    {
        public static void SetBaseRequest(this BaseRequest request)
        {
            request.SetBaseRequest(Guid.NewGuid().ToString());
        }

        public static void SetBaseRequest(this BaseRequest request, string group)
        {
            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.Domain = ConfigSettingReader.Domain;
            request.Initialise = true;
            request.AuditData = new AuditData()
            {
                BrowserType = HttpContext.Current.Request.UserAgent + HttpContext.Current.Request.Browser,
                ClientIpAddress = HttpContext.Current.Request.UserHostAddress,
                GroupingId = group,
                HostName = HttpContext.Current.Request.UserHostName,
                SessionId = HttpContext.Current.Session.SessionID,
                Username = HttpContext.Current.User.Identity.Name
            };
        }
    }
}
