namespace Paramount.DSL.UI
{
    using System;
    using System.Web;
    using System.Web.UI.WebControls;
    using Paramount.Common.UI.BaseControls;
    using Paramount.DSL.UIController;

    public abstract class DslThumbImage : ParamountCompositeControl
    {
        private readonly ImageButton _imageThumb;

        public event EventHandler ImageSelect;

        private void InvokeOnItemSelect(EventArgs e)
        {
            var handler = ImageSelect;
            if (handler != null) handler(this, e);
        }

        protected DslThumbImage()
        {
            _imageThumb = new ImageButton { CssClass = "dslthumbimage-imagethumb" };
            _imageThumb.Click += HyperLinkServerClick;
        }

        void HyperLinkServerClick(object sender, EventArgs e)
        {
            InvokeOnItemSelect(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // Create the parent panel
            var thumbPanel = new Panel { CssClass = "dslthumbimage-thumbpanel" };
            thumbPanel.Controls.Add(_imageThumb);
            Controls.Add(thumbPanel);
        }

        public string DocumentId
        {
            get
            {
                return ViewState["documentId"].ToString();
            }
            set
            {
                DslQueryParam param = new DslQueryParam(HttpContext.Current.Request.QueryString)
                {
                    Entity = GetEntityCode(),
                    Height = GetThumbHeight(),
                    Width = GetThumbWidth(),
                    Resolution = GetResolution(),
                    DocumentId = value
                };

                string dslHandlerUrl = GetDslHandlerUrl();
                _imageThumb.ImageUrl = param.GenerateUrl(dslHandlerUrl);

                ViewState["documentId"] = value;
            }
        }

        public abstract string GetEntityCode();
        public abstract string GetDslHandlerUrl();
        public abstract int GetThumbWidth();
        public abstract int GetThumbHeight();
        public abstract int GetResolution();
    }
}
