using System.ServiceModel;
using System.Web;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Common.DataTransferObjects;

namespace Paramount.Services
{
    public class BaseService
    {
        public ServiceInformation GetApplicationInformation()
        {

            var app = HttpContext.Current.ApplicationInstance as IServiceInformation;
            if( app != null)
            {
                return new ServiceInformation
                           {
                               ApplicationName = app.ApplicationName,
                               Version = app.Version
                           };
            }
            return new ServiceInformation
                       {
                           ApplicationName = ConfigSettingReader.ApplicationName,
                           Version = this.GetType().Assembly.GetName().Version.ToString()
                       };
        }
    }
}