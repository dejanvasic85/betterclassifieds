using System.Collections.Generic;
using Paramount.Common.UI.BaseControls;
using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BetterClassified.UI
{
    public class TagsInput : CompositeControl
    {
        private readonly HtmlInputText inputText;
        private readonly HtmlInputHidden hiddenField;

        public TagsInput()
        {
            inputText = new HtmlInputText();
            inputText.Attributes.Add("class", "tags");

            hiddenField = new HtmlInputHidden();
            hiddenField.Attributes.Add("class", "hdnInputTag");
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
            Controls.Add(inputText);
            Controls.Add(hiddenField);
        }

        public string GetTags()
        {
            return hiddenField.Value;
        }

        public void SetTags(string tags)
        {
            inputText.Value = tags;
            hiddenField.Value = tags;
        }
    }
}