using System.Collections.Generic;
using Paramount.Common.UI.BaseControls;
using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BetterClassified.UI
{
    public class TagsInput : CompositeControl
    {
        private readonly HtmlInputText _inputText;
        private readonly HtmlInputHidden _hiddenField;

        public TagsInput()
        {
            _inputText = new HtmlInputText();
            _inputText.Attributes.Add("class", "tags");

            _hiddenField = new HtmlInputHidden();
            _hiddenField.Attributes.Add("class", "hdnInputTag");
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            // Include the plugin script
            Page.ClientScript.RegisterClientScriptResource(this.GetType(), "BetterClassified.UI.Controls.TagsInput.jquery.tagsinput.js");
            Page.AddCssResource(this.GetType(), "BetterClassified.UI.Controls.TagsInput.jquery.tagsinput.css");
        }

        protected override void CreateChildControls()
        {
            // Add the input control 
            Controls.Add(_inputText);
            Controls.Add(_hiddenField);
        }

        public string GetTags()
        {
            return _hiddenField.Value;
        }

        public void SetTags(string tags)
        {
            _inputText.Value = tags;
            _hiddenField.Value = tags;
        }

        public string AddTagMethodName
        {
            get { return _hiddenField.Attributes["data-addservicename"]; }
            set { _hiddenField.Attributes["data-addservicename"] = value; }
        }

        public string AutoCompleteMethodName
        {
            get { return _hiddenField.Attributes["data-autocompleteurl"]; }
            set { _hiddenField.Attributes["data-autocompleteurl"] = value; }
        }
    }
}