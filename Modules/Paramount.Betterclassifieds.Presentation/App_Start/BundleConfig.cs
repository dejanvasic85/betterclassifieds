using System.Web.Optimization;

namespace Paramount.Betterclassifieds.Presentation.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Styles
            bundles.Add(new Bundle("~/styles/default").Include("~/Content/bootstrap.css", "~/Content/site.css"));
            bundles.Add(new Bundle("~/styles/themusic").Include("~/Content/bootstrap.css", "~/Content/TheMusic/themusic.css"));

            // Javascript
            bundles.Add(new Bundle("~/scripts/default").Include("~/Scripts/jquery-1.11.0.js", "~/Scripts/bootstrap.js"));


        }
    }
}