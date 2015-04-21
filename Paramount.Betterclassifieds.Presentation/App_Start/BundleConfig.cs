using System.Web.Optimization;

namespace Paramount.Betterclassifieds.Presentation.App_Start
{
    public class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            // The jQuery library
            bundles.Add(new ScriptBundle("~/jquery")
                .Include("~/Scripts/jquery-1.*"));

            // jQuery modules
            bundles.Add(new ScriptBundle("~/jquery-modules")
                .Include("~/Scripts/jquery.*"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bs")
                .Include("~/Scripts/bootstrap-3.3.0.js")
                .Include("~/Scripts/bootstrap-datepicker.js"));

            // Knockout library
            bundles.Add(new ScriptBundle("~/ko")
                .Include("~/Scripts/knockout-3.2.0.js"));

            // Components - all others
            bundles.Add(new ScriptBundle("~/components")
                .Include("~/Scripts/toastr-*")
                .Include("~/Scripts/moment-*")
                .Include("~/Scripts/mobile-detect.js")
                .Include("~/Scripts/cropper.js")
                );

            bundles.Add(new ScriptBundle("~/bundle/paramount")
                .Include("~/Scripts/paramount-site.js")
                .Include("~/Scripts/paramount-ui.js")
                .Include("~/Scripts/paramount-*")
                .IncludeDirectory("~/Scripts/Account/", "*.js")
                .IncludeDirectory("~/Scripts/Booking/", "*.js")
                .IncludeDirectory("~/Scripts/Listings/", "*.js")
                );

            // Enable this flag for development only when trying to force bundling and minification
            //BundleTable.EnableOptimizations = true;
        }
    }
}