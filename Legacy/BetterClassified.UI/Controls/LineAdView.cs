using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.UI.BaseControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;

namespace BetterClassified.UI
{
    public class LineAdView : ParamountCompositeControl
    {
        private Panel _pnlContainer;
        private Panel _pnlHeader;
        private Label _lblHeaderText;
        private Panel _pnlAdText;
        private Label _lblAdText;
        private Panel _pnlImage;
        private Image _imgPrintPreview;

        public LineAdView()
        {
            // Instantiate controls here 
            _pnlContainer = new Panel { CssClass = "lineadview-container " };
            _pnlHeader = new Panel { CssClass = "lineadview-header" };
            _pnlImage = new Panel { CssClass = "lineadview-image" };
            _pnlAdText = new Panel { CssClass = "lineadview-mainText" };

            _lblHeaderText = new Label();
            _lblAdText = new Label();
            _imgPrintPreview = new Image();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            const string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            string includeLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.Resources.lineAdView.css");
            var include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (this.IsHeaderVisible)
            {
                _pnlHeader.Controls.Add(_lblHeaderText);
                _pnlContainer.Controls.Add(_pnlHeader);
            }

            if (this.IsImageVisible)
            {
                _pnlImage.Controls.Add(_imgPrintPreview);
                _pnlContainer.Controls.Add(_pnlImage);
            }

            _pnlAdText.Controls.Add(_lblAdText);
            _pnlContainer.Controls.Add(_pnlAdText);
            this.Controls.Add(_pnlContainer);
        }

        [DefaultValue("True")]
        public bool IsHeaderVisible
        {
            get
            {
                EnsureChildControls();
                return _pnlHeader.Visible;
            }
            set
            {
                EnsureChildControls();
                _pnlHeader.Visible = value;
            }
        }

        public string HeaderText
        {
            get
            {
                EnsureChildControls();
                return _lblHeaderText.Text;
            }
            set
            {
                EnsureChildControls();
                _lblHeaderText.Text = value;
            }
        }

        public string HeaderColourCode
        {
            get
            {
                EnsureChildControls();
                return System.Drawing.ColorTranslator.ToHtml(_lblHeaderText.ForeColor);
            }
            set
            {
                EnsureChildControls();
                _lblHeaderText.ForeColor = System.Drawing.ColorTranslator.FromHtml(value);
            }
        }

        public bool IsHeadingSuperBold
        {
            get
            {
                EnsureChildControls();
                return bool.Parse(ViewState["isHeadingSuperBold"].ToString());
            }
            set
            {
                ViewState["isHeadingSuperBold"] = value;
                _pnlHeader.CssClass = value == true ? "lineadview-superboldheader" : "lineadview-header";
            }
        }

        public string AdText
        {
            get
            {
                EnsureChildControls();
                return _lblAdText.Text;
            }
            set
            {
                EnsureChildControls();
                _lblAdText.Text = value;
            }
        }

        [DefaultValue("True")]
        public bool IsImageVisible
        {
            get
            {
                EnsureChildControls();
                return _pnlImage.Visible;
            }
            set
            {
                EnsureChildControls();
                _pnlImage.Visible = value;
            }
        }

        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design,Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor)), UrlProperty]
        public string ImageUrl
        {
            get
            {
                EnsureChildControls();
                return _imgPrintPreview.ImageUrl;
            }
            set
            {
                EnsureChildControls();
                _imgPrintPreview.ImageUrl = value;
            }
        }

        public string BackgroundColourCode
        {
            get
            {
                EnsureChildControls();
                return System.Drawing.ColorTranslator.ToHtml(this._pnlContainer.BackColor);
            }
            set
            {
                EnsureChildControls();
                this._pnlContainer.BackColor = System.Drawing.ColorTranslator.FromHtml(value);
            }
        }

        public string BorderColourCode
        {
            get
            {
                EnsureChildControls();
                return System.Drawing.ColorTranslator.ToHtml(this._pnlContainer.BorderColor);
            }
            set
            {
                EnsureChildControls();
                this._pnlContainer.BorderColor = System.Drawing.ColorTranslator.FromHtml(value);
                this._pnlContainer.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                this._pnlContainer.BorderWidth = Unit.Pixel(2);
            }
        }
    }
}
