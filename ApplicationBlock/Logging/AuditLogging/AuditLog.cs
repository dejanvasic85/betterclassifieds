using Paramount.ApplicationBlock.Configuration;

namespace Paramount.ApplicationBlock.Logging.AuditLogging
{
    using System;
    using System.Web;
    using Enum;

    public class AuditLog : BaseClass.LogBase
    {
        public AuditLog()
            : this(ConfigManager.GetApplicationName())
        {
            
        }

        public AuditLog(string applicationName)
        {
            this.DateTimeCreated = DateTime.Now;
            this.Application = applicationName;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.User != null)
                {
                    this.User = HttpContext.Current.User.Identity.Name;
                }
                if (HttpContext.Current.Session != null)
                {
                    this.SessionId = HttpContext.Current.Session.SessionID;
                }
                if(HttpContext.Current.Request != null)
                {
                    this.HostName = HttpContext.Current.Request.UserHostName;
                    this.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    if(string.IsNullOrEmpty(this.User))
                    {
                        this.User = HttpContext.Current.Request.LogonUserIdentity.Name;
                    }
                }
            }
        }

        
        public override CategoryTypes Category
        {
            get { return CategoryTypes.AuditLog; }
        }
    }
}