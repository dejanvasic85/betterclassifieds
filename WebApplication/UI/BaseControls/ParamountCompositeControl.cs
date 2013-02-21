namespace Paramount.Common.UI.BaseControls
{
    using System.Web;
    using System.Web.UI.WebControls;
    using Common.UI;
    using Common.UI.WebContent;

    public abstract class ParamountCompositeControl : CompositeControl
    {
        public string Domain { get; set; }

        public string GetResource(EntityGroup group, ContentItem item, string fieldId)
        {
            return WebContentManager.GetResource(group, item, fieldId);
        }

        public string GetResources(EntityGroup group, ContentItem item, string fieldId)
        {
            return WebContentManager.GetResource(group, item, fieldId);
        }
        public string GetQueryString(string key)
        {
            return HttpContext.Current.Request[HttpContext.Current.Server.UrlDecode(key)];
        }

        public bool IsInPdfPrintMode
        {
            get
            {
                if (this.Page is IPdfPrint)
                {
                    return (this.Page.Request.QueryString["pdf"] == "true");
                }
                return false;
            }
        }

      
    }
}