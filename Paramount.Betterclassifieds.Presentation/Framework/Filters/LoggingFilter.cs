using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogService _logService;
        public LoggingFilter()
        {
            _logService = DependencyResolver.Current.GetService<ILogService>();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var httpRequestBase = filterContext.RequestContext.HttpContext.Request;
                var method = httpRequestBase.HttpMethod;
                if (method != "POST")
                    return;

                var data =
                    new StringBuilder(
                        $"POST {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{filterContext.ActionDescriptor.ActionName}\n");

                // Print the request data
                foreach (var actionParameter in filterContext.ActionParameters)
                {
                    using (var str = new StringWriter())
                    {
                        var s = new JsonSerializer();
                        s.Serialize(str, actionParameter.Value);
                        data.AppendLine($"\n\tParam: {actionParameter.Key}");
                        data.AppendLine($"\tValue: {str}");
                    }
                }

                _logService.Info(data.ToString());
            }
            catch (Exception ex)
            {
                _logService.Error("Unable to serialize data", ex);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}