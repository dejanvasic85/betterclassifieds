using System.Web.Optimization;

namespace Paramount.Betterclassifieds.Presentation
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
                    
            bundles.Add(new ScriptBundle("~/bundle/ps")
                .Include("~/Scripts/paramount/paramount-site.js")
                .Include("~/Scripts/paramount/paramount-ui.js")
                .Include("~/Scripts/paramount/paramount-*"));

            bundles.Add(new ScriptBundle("~/bundle/paramount-booking").IncludeDirectory("~/Scripts/Booking", "*.js"));
            bundles.Add(new ScriptBundle("~/bundle/paramount-account").IncludeDirectory("~/Scripts/Account", "*.js"));
            bundles.Add(new ScriptBundle("~/bundle/paramount-listings").IncludeDirectory("~/Scripts/Listings", "*.js"));

            
            // Enable this flag for development only when trying to force bundling and minification
            //BundleTable.EnableOptimizations = true;
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/css/base")
                .Include("~/Content/bootstrap-3.3.0.css"));
        }
    }
}