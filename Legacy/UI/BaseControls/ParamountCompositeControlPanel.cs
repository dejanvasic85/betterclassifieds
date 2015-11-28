namespace Paramount.Common.UI.BaseControls
{
    using System.Web.UI;

    public abstract class ParamountCompositeControlPanel : ParamountCompositeControl
    {
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }
    }
}
