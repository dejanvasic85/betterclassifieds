using System.Web.Mvc;
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
            var httpRequestBase = filterContext.RequestContext.HttpContext.Request;
            var method = httpRequestBase.HttpMethod;
            if (method != "POST")
                return;

            _logService.Info($"POST {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{filterContext.ActionDescriptor.ActionName}");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}