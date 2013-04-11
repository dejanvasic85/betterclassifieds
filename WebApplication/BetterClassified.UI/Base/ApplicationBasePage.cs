namespace BetterClassified.UI.BaseControl
{
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;

    public abstract class ApplicationBasePage:Page
    {
        protected override void OnPreInit(System.EventArgs e)
        {
            base.OnPreInit(e);
            MasterPageFile =ResolveClientUrl( string.Format( "~/{0}", Path.Combine(PageSettings.MasterPagePath, MasterPage)));
        }
        protected abstract string MasterPage
        { 
            get;
        }
    }
}