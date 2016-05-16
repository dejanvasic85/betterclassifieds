using System.IO;
using System.Web;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation
{
    public class LoggingConfig
    {
        public static void Register(ILogService logService)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(
                HttpContext.Current.Server.MapPath("~/Web.config")));

            logService.Info("Application Started. Logging configured successfully.");
        }
    }
}