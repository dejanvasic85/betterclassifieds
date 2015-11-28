namespace Paramount.Common.UI
{
    using System.Web.UI.HtmlControls;
    using BaseControls;

    public class HeaderControl : ParamountCompositeControl
    {
        public string Title { get; set; }
        private readonly HtmlGenericControl _header;

        public HeaderControl()
        {
            _header = new HtmlGenericControl("h3");
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            _header.InnerText = Title;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(_header);
        }
    }
}
