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
                .Include("~/Scripts/knockout/knockout-3.2.0.js"));

            // Components - all others
            bundles.Add(new ScriptBundle("~/bundle/vendor").IncludeDirectory("~/Scripts/vendor", "*.js"));

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
            BundleTable.EnableOptimizations = false;
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