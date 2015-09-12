using System.Web.Optimization;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            // The jQuery library
            bundles.Add(new ScriptBundle("~/bundle/jquery")
                .Include("~/Scripts/jquery/jquery-1.*"));

            // Knockout library
            bundles.Add(new ScriptBundle("~/bundle/ko")
                .IncludeDirectory("~/Scripts/knockout", "*.js"));

            // Components - all others
            bundles.Add(new ScriptBundle("~/bundle/vendor").IncludeDirectory("~/Scripts/vendor", "*.js", false));

            // Paramount            
            bundles.Add(new ScriptBundle("~/bundle/paramount-app")
                .Include("~/Scripts/app-global/paramount-site.js")
                .Include("~/Scripts/app-global/paramount-ui.js")
                .IncludeDirectory("~/Scripts/app-global", "*.js", true)
                .IncludeDirectory("~/Scripts/app", "*.js", true)
                );
            
            // Enable this flag for development only when trying to force bundling and minification
            BundleTable.EnableOptimizations = false;
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Content/css-base")
                .Include("~/Content/bootstrap-3.3.0.css")
                .Include("~/Content/font-awesome-4.4.0.css")
                .Include("~/Content/site.css")
                .Include("~/Content/bootstrap-select.css")
                .Include("~/Content/datepicker.css")
                .Include("~/Content/datepicker3.css")
                .Include("~/Content/jquery.fileupload-1.3.0.css")
                .Include("~/Content/morphext.css")
                .Include("~/Content/animate.css")
                .Include("~/Content/cropper.css")     
                .IncludeDirectory("~/Content/components", "*.css")
                );

        }
    }
}