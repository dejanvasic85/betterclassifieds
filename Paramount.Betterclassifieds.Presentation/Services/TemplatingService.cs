using System;
using System.IO;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface ITemplatingService
    {
        ITemplatingService Init(Controller controller); // Must be called
        string Generate<TViewModel>(TViewModel model, string viewTarget) where TViewModel : new();
    }

    public class TemplatingService : ITemplatingService
    {
        private Controller _controller;
        
        public ITemplatingService Init(Controller controller)
        {
            _controller = controller;
            return this;
        }

        public string Generate<TViewModel>(TViewModel model, string viewTarget) where TViewModel : new()
        {
            if (_controller == null)
            {
                throw new NullReferenceException("Please call the Init method first passing in the Controller");
            }

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