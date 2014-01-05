namespace Paramount.ApplicationBlock.Mvc
{
    using ApplicationBlock.Configuration;
    using System.Web.Mvc;
    using System.Linq;

    public class ClientViewEngine : RazorViewEngine
    {
        public ClientViewEngine(string baseUrl)
        {
            // Set default view location format
            this.ViewLocationFormats = new[]
            {
                baseUrl + "/Views/{1}/{0}.cshtml",
                baseUrl + "/Views/Shared/{1}/{0}.cshtml"
            };
            
            // Set default master location formats
            this.MasterLocationFormats = new[]
            {
                baseUrl + "/Views/Shared{0}.cshtml",
                baseUrl + "/Views/{0}.cshtml"
            };

            // Setup client specific view conventions
            var brand = ConfigManager.ReadAppSetting("Brand");
            if (brand.HasValue())
            {
                this.ViewLocationFormats = new[]
                {
                    baseUrl + "/Views/{1}/{0}." + brand + ".cshtml",
                    baseUrl + "/Views/Shared/{1}/{0}." + brand + ".cshtml"
                }.Union(this.ViewLocationFormats).ToArray();
                
                this.MasterLocationFormats = new[]
                {
                    baseUrl + "/Views/Shared/{0}." + brand + ".cshtml",
                    baseUrl + "/Views/{0}." + brand + ".cshtml"
                }
                .Union(this.MasterLocationFormats)
                .ToArray();
            }

            // Partial formats are just same as view formats
            this.PartialViewLocationFormats = this.ViewLocationFormats;
        }

        // Constructor chaining ( default as if it's a normal application - non module)
        public ClientViewEngine()
            : this("~")
        { }
    }
}
