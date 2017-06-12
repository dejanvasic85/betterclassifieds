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
            bundles.Add(new ScriptBundle("~/bundle/vendor")
                .Include("~/Scripts/vendor/moment-2.18.1.js")            // This needs to be loaded first (or before bootstrap-datepicker)
                .IncludeDirectory("~/Scripts/vendor", "*.js", false));

            // Paramount            
            bundles.Add(new ScriptBundle("~/bundle/paramount-app")
                .Include("~/Scripts/app-global/paramount-site.js")
                .Include("~/Scripts/app-global/paramount-ui.js")
                .IncludeDirectory("~/Scripts/app-global", "*.js", true) // Infrastructure and utiltiy functions (e.g. UI button loaders)
                .IncludeDirectory("~/Scripts/app/common", "*.js", true) // All the global objects go here
                .IncludeDirectory("~/Scripts/app/ui", "*.js", true)     // All ui (jQuery specific) stuff goes here that may reference view models (app)
                .IncludeDirectory("~/Scripts/app", "*.js", true)        // All the knockout objects
                );

            // Enable this flag for development only when trying to force bundling and minification
            //BundleTable.EnableOptimizations = true;
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Content/css-base")
                .Include("~/Content/font-awesome-4.6.1.css")
                .Include("~/Content/bootstrap-select.css")
                .Include("~/Content/datepicker.css")
                .Include("~/Content/datepicker3.css")
                .Include("~/Content/jquery.fileupload-1.3.0.css")
                .Include("~/Content/morphext.css")
                .Include("~/Content/animate.css")
                .Include("~/Content/cropper.css")
                .Include("~/Content/toastr-*")
                .Include("~/Content/site.css")
                .Include("~/Content/site-bs-override.css")
                .IncludeDirectory("~/Content/components", "*.css")
                .Include("~/Scripts/app/events/seatSelector/seatSelector.css")
            );

            bundles.Add(new Bundle("~/Content/Kandobay/styles")
                .IncludeDirectory("~/Content/KandoBay/", "*.css"));
        }
    }
}