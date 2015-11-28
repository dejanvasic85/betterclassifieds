namespace Paramount.Common.UI
{
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public class ParamountLabel : Label
    {
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Label; }
        }
    }
}
