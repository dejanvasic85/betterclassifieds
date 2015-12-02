using System.IO;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface ITemplatingService
    {
        string Generate<TViewModel>(TViewModel model, string viewTarget) where TViewModel : new();
    }

    public class TemplatingService : ITemplatingService
    {
        private readonly Controller _controller;

        public TemplatingService(Controller controller)
        {
            _controller = controller;
        }

        public string Generate<TViewModel>(TViewModel model, string viewTarget) where TViewModel : new()
        {
            using (var writer = new StringWriter())
            {
                _controller.ViewData.Model = model;
                var result = ViewEngines.Engines.FindPartialView(_controller.ControllerContext, viewTarget);
                var viewContext = new ViewContext(_controller.ControllerContext, result.View, _controller.ViewData, _controller.TempData, writer);
                result.View.Render(viewContext, writer);
                result.ViewEngine.ReleaseView(_controller.ControllerContext, result.View);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}