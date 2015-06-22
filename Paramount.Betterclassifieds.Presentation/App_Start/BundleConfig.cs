using System.Web.Optimization;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            // The jQuery library
            bundles.Add(new ScriptBundle("~/bundle/jquery")
                .Include("~/Scripts/vendor/jquery-1.*"));

            // jQuery modules
            bundles.Add(new ScriptBundle("~/bundle/jquery-modules")
                .Include("~/Scripts/vendor/jquery.*"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundle/bootstrap")
                .Include("~/Scripts/vendor/bootstrap-3.3.0.js")
                .Include("~/Scripts/vendor/bootstrap-datepicker.js"));

            // Knockout library
            bundles.Add(new ScriptBundle("~/bundle/ko")
                .Include("~/Scripts/vendor/knockout-3.2.0.js"));

            // Components - all others
            bundles.Add(new ScriptBundle("~/bundle/components").IncludeDirectory("~/Scripts/vendor", "*.js"));

            // Paramount            
            bundles.Add(new ScriptBundle("~/bundle/paramount-gl")
                .Include("~/Scripts/paramount/paramount-site.js")
                .Include("~/Scripts/paramount/paramount-ui.js")
                .Include("~/Scripts/paramount/paramount-*"));

            bundles.Add(new ScriptBundle("~/bundle/paramount-booking").IncludeDirectory("~/Scripts/Booking", "*.js"));
            bundles.Add(new ScriptBundle("~/bundle/paramount-account").IncludeDirectory("~/Scripts/Account", "*.js"));
            bundles.Add(new ScriptBundle("~/bundle/paramount-listings").IncludeDirectory("~/Scripts/Listings", "*.js"));
            bundles.Add(new ScriptBundle("~/bundle/events").IncludeDirectory("~/Scripts/app/events", "*.js"));
            
            // Enable this flag for development only when trying to force bundling and minification
            //BundleTable.EnableOptimizations = true;
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Content/css-base")
                .Include("~/Content/bootstrap-3.3.0.css")
                .Include("~/Content/font-awesome-4.3.0.css")
                .Include("~/Content/site.css")
                .Include("~/Content/bootstrap-select.css")
                .Include("~/Content/datepicker.css")
                .Include("~/Content/datepicker3.css")
                .Include("~/Content/jquery.fileupload-1.3.0.css")
                .Include("~/Content/morphext.css")
                .Include("~/Content/animate.css")
                .Include("~/Content/cropper.css")
                );

        }
    }
}