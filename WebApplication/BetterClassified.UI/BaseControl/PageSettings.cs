namespace BetterClassified.UI.BaseControl
{
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;

    public static class PageSettings
    {
        public static string Theme
        {
            get
            {
                var pagesSection = ConfigurationManager.GetSection("system.web/pages") as PagesSection;
                return pagesSection == null ? string.Empty : pagesSection.Theme;
            }
        }

        public static string MasterPagePath
        {
            get
            {
                var masterPagePath = ConfigurationManager.AppSettings["MasterPagePath"];
                var tempPath = Path.Combine(masterPagePath, Theme);

                return Directory.Exists(HttpContext.Current.Server.MapPath(string.Format("~/{0}",tempPath))) ? tempPath : masterPagePath;
            }
        }
    }
}