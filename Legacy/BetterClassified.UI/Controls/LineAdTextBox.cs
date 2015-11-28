using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using BetterClassified.UIController;
using BetterClassified.UIController.Booking;
using BetterclassifiedsCore;
using Paramount.Betterclassified.Utilities.Configuration;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;

namespace BetterClassified.UI
{
    public class LineAdTextBox : ParamountCompositeControl
    {
        private readonly TextBox _adBodyTextBox;
        private readonly Panel _bottomToolbarPanel;
        private readonly Label _wordCountLabel;
        private readonly Label _wordCountValue;
        private readonly Label _maxWordCountLabel;

        public LineAdTextBox()
        {
            _adBodyTextBox = new TextBox
            {
                CssClass = "adBodyTextBox",
                TextMode = TextBoxMode.MultiLine,
                AutoCompleteType = AutoCompleteType.None
            };
            _bottomToolbarPanel = new Panel { CssClass = "bottomToolbarPanel" };
            _wordCountLabel = new Label
            {
                CssClass = "wordCountLabel",
                Text = string.Format("{0}: ",GetResource(EntityGroup.Betterclassified, ContentItem.LineAdTextBoxControl, "Wordcount.Text"))
            };
            _wordCountValue = new Label { CssClass = "wordCountValue", Text = @"0" };
            _maxWordCountLabel = new Label { CssClass = "maxWordCountValue" };
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _bottomToolbarPanel.Controls.Add(_wordCountLabel);
            _bottomToolbarPanel.Controls.Add(_wordCountValue);

            Controls.Add(_adBodyTextBox);
            Controls.Add(_bottomToolbarPanel);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Page.ClientScript.RegisterClientScriptResource(GetType(), "BetterClassified.UI.JavaScript.linead-textbox.js");

            const string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            var includeLocation = Page.ClientScript.GetWebResourceUrl(GetType(), "BetterClassified.UI.Resources.lineAdTextBox.css");
            var include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);

            if (!MaximumWords.HasValue) return;
            _bottomToolbarPanel.Controls.Add(_maxWordCountLabel);
            _maxWordCountLabel.Text = string.Format("Word Limit: {0}", MaximumWords);
        }

        #region Properties

        public int? MaximumWords
        {
            get { return (int?)ViewState["maximumWords"]; }
            set { ViewState["maximumWords"] = value; }
        }

        public int WordCount
        {
            get
            {
                return LineAdHelper.GetWordCount(AdBodyText);
            }
        }

        public string AdBodyText
        {
            get
            {
                return _adBodyTextBox.Text;
            }
            set
            {
                _adBodyTextBox.Text = value;
            }
        }

        #endregion
    }
}
