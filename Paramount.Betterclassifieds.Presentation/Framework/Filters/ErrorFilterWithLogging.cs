using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Framework
{
    public class ErrorFilterWithLogging : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            // Let the pipeline know that exception has been handled
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Error", "ServerProblem");

            // Serve down our view
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/ServerProblem.cshtml",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}