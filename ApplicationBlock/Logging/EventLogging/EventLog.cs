using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.ApplicationBlock.Logging.BaseClass;
using Paramount.ApplicationBlock.Logging.Enum;
using Paramount.ApplicationBlock.Logging.Interface;
using Paramount.ApplicationBlock.Configuration;

namespace Paramount.ApplicationBlock.Logging.EventLogging
{
    [Serializable]
    public class EventLog : LogBase
    {
        private const string ApplicationName = "Application";

        public EventLog(string errorMessage):this(new Exception(errorMessage))
        {}

        public EventLog(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentException("execption cannot be null", "exception");
            }
            this.DateTimeCreated = DateTime.Now;
            this.Application = ConfigSettingReader.ApplicationName;
            this.Domain = ConfigSettingReader.Domain;

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
                if (HttpContext.Current.Request != null)
                {
                    this.HostName = HttpContext.Current.Request.UserHostName;
                    this.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    if (string.IsNullOrEmpty(this.User))
                    {
                        this.User = HttpContext.Current.Request.LogonUserIdentity.Name;
                    }
                }
            }
            this.Data = exception.ToString();
            if(exception.InnerException != null)
            {
                this.SecondaryData =exception.InnerException.ToString();
            }
        }
        public override CategoryTypes Category
        {
            get { return CategoryTypes.EventLog; }
        }
        
    }

}
