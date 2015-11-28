namespace Paramount.Common.UI
{
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using BaseControls;

    public class ParamountHtmlGenericControl:ParamountCompositeControl
    {
        private readonly HtmlGenericControl _control;
        public ParamountHtmlGenericControl():this (string.Empty)
        {
            
        }

        public ParamountHtmlGenericControl(string tag)
        {
            _control = new HtmlGenericControl(tag);
        }

        public override ControlCollection Controls
        {
            get
            {
                return _control.Controls ;
            }
        }

        protected override void CreateChildControls()
        {
            _control.Attributes.Add("class", CssClass);
            CssClass = string.Empty;
            base.Controls.Add(_control); 
            base.CreateChildControls();
        }

    }
}