namespace Paramount.Common.UI
{
    using System.Web.UI;
    using Common.UI.BaseControls;

    public class ListItemPanel : ParamountCompositeControl
    {
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Li; }
        }
    }
}
