using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paramount.Common.UI.BaseControls;
using System.ComponentModel;

namespace Paramount.Common.UI
{
    public class GenericMessagePanel : ParamountCompositeControl
    {
        private Panel _pnlContainer;
        private Literal _lblText;
        private MessagePanelType _messagePanelType;

        private const string BackgroundCss = "background:transparent url({0}) no-repeat scroll 10px 13px;";

        public GenericMessagePanel()
        {
            _pnlContainer = new Panel();
            _lblText = new Literal();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (!string.IsNullOrEmpty(MessageText) || this.IsAlwaysVisible)
            {

                _lblText.Text = MessageText;
                _messagePanelType = MessageType;

                switch (_messagePanelType)
                {
                    case MessagePanelType.Success:
                        _pnlContainer.CssClass = "success genericmessagepanel-msg";
                        _pnlContainer.Attributes.Add("style", string.Format(BackgroundCss, GetSuccessImage()));
                        break;
                    case MessagePanelType.Information:
                        _pnlContainer.CssClass = "information genericmessagepanel-msg";
                        _pnlContainer.Attributes.Add("style", string.Format(BackgroundCss, GetInformationImage()));
                        break;
                    case MessagePanelType.Warning:
                        _pnlContainer.CssClass = "warning genericmessagepanel-msg";
                        _pnlContainer.Attributes.Add("style", string.Format(BackgroundCss, GetWarningImage()));
                        break;
                    case MessagePanelType.Error:
                        _pnlContainer.CssClass = "error genericmessagepanel-msg";
                        _pnlContainer.Attributes.Add("style", string.Format(BackgroundCss, GetErrorImage()));
                        break;
                    case MessagePanelType.Suggestion:
                        _pnlContainer.CssClass = "suggestion genericmessagepanel-msg";
                        _pnlContainer.Attributes.Add("style", string.Format(BackgroundCss, GetSuggestionImage()));
                        break;
                    default: break;
                }

                // Add controls
                _pnlContainer.Controls.Add(_lblText);

                this.Controls.Add(_pnlContainer);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            const string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            string includeLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Paramount.Common.UI.CSS.GenericMessagePanel.css");
            var include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);
        }

        public MessagePanelType MessageType { get; set; }
        public string MessageText { get; set; }
        public bool IsAlwaysVisible { get; set; }

        private string GetSuccessImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), 
                "Paramount.Common.UI.Images.success.png");
        }

        private string GetErrorImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), 
                "Paramount.Common.UI.Images.error.png");
        }

        private string GetInformationImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), 
                "Paramount.Common.UI.Images.information.png");
        }

        private string GetSuggestionImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), 
                "Paramount.Common.UI.Images.suggestion.png");
        }

        private string GetWarningImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), 
                "Paramount.Common.UI.Images.warning.png");
        }
    }

    public enum MessagePanelType
    {
        Success,
        Error,
        Information,
        Warning,
        Suggestion
    }
}
