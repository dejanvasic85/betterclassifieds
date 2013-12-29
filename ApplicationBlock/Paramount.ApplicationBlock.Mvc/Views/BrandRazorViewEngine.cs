namespace Paramount.ApplicationBlock.Mvc
{
    using System.Web.Mvc;
    using System.Linq;
    using ApplicationBlock.Configuration;

    public class BrandRazorViewEngine : RazorViewEngine
    {
        private string _placeholder = "%1";
        private string _appKey = "Brand";

        public BrandRazorViewEngine() : base()
        {
            //ViewLocationFormats = ViewLocationFormats.Concat(new[] { "~/Views/{1}/%1/{0}.cshtml" }).ToArray();
            ViewLocationFormats = new[] {"~/Views/{1}/%1/{0}.cshtml"};
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var brand = ConfigManager.ReadAppSetting(_appKey);
            if (brand.IsNullOrEmpty())
                return base.CreateView(controllerContext, viewPath, masterPath);

            var viewPathWithBrand = viewPath.Replace(_placeholder, brand);
            return base.CreateView(controllerContext, viewPathWithBrand, masterPath);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            var virtualPathWithBrand = virtualPath.Replace(_placeholder, ConfigManager.ReadAppSetting(_appKey));
            return base.FileExists(controllerContext, virtualPathWithBrand);
        }
    }
}
