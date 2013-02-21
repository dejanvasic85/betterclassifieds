using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Common.DataTransferObjects;

namespace Paramount.Billing.UIController
{
    public static class Extensions
    {
        public static void SetBaseRequest(this BaseRequest request, string group)
        {
            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.Domain = ConfigSettingReader.Domain;
            request.Initialise = true;

            if (HttpContext.Current != null)
            {
                request.AuditData = new AuditData()
                                        {
                                            BrowserType =
                                                HttpContext.Current.Request.UserAgent +
                                                HttpContext.Current.Request.Browser,
                                            ClientIpAddress = HttpContext.Current.Request.UserHostAddress,
                                            GroupingId = group,
                                            HostName = HttpContext.Current.Request.UserHostName,
                                            SessionId = (HttpContext.Current.Session == null) ? string.Empty : HttpContext.Current.Session.SessionID,
                                            Username = HttpContext.Current.User.Identity.Name
                                        };
            }
        }
    }
}
