 namespace Paramount.DSL.UI
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Paramount.Common.UI.BaseControls;
    using Paramount.DSL.UIController;

    public abstract class DslThumbCommand : ParamountCompositeControl
    {
        private readonly Image _image;
        private readonly LinkButton _commandButton;
        private readonly UpdatePanel _updatePanel;

        public event EventHandler CommandClick;

        private void InvokeOnItemCommand(EventArgs e)
        {
            var handler = CommandClick;
            if (handler != null) handler(this, e);
        }

        protected DslThumbCommand()
        {
            _image = new Image { CssClass = "dslthumbcommand-image" };
            _commandButton = new LinkButton { CssClass = "dslthumbcommand-commandbutton" };
            _commandButton.Click += CommandServerClick;
        }

        void CommandServerClick(object sender, EventArgs e)
        {
            InvokeOnItemCommand(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            var thumbPanel = new Panel { CssClass = "dslthumbcommand-thumbpanel" };
            thumbPanel.Controls.Add(_image);

            var commandPanel = new Panel() { CssClass = "dslthumbcommand-commandpanel" };
            commandPanel.Controls.Add(_commandButton);

            Controls.Add(thumbPanel);
            Controls.Add(commandPanel);
        }

        public string DocumentId
        {
            get
            {
                return ViewState["documentId"].ToString();
            }
            set
            {
                var param = new DslQueryParam(HttpContext.Current.Request.QueryString)
                {
                    Entity = GetEntityCode(),
                    Height = GetThumbHeight(),
                    Width = GetThumbWidth(),
                    Resolution = GetResolution(),
                    DocumentId = value
                };

                string dslHandlerUrl = GetDslHandlerUrl();
                _image.ImageUrl = param.GenerateUrl(dslHandlerUrl);

                ViewState["documentId"] = value;
            }
        }

        public string CommandText
        {
            get
            {
                return _commandButton.Text;
            }
            set
            {
                _commandButton.Text = value;
            }
        }

        public abstract string GetEntityCode();
        public abstract string GetDslHandlerUrl();
        public abstract int GetThumbWidth();
        public abstract int GetThumbHeight();
        public abstract int GetResolution();
    }
}
