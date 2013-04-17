using System.Web.UI.WebControls;

namespace BetterClassified.UI
{
    public class FormLabel : CompositeControl
    {
        // Inner controls
        private readonly Panel divContainer;
        private readonly Label labelTitle;
        private readonly Label help;
        private readonly Label labelValue;

        // Constructor
        public FormLabel()
        {
            divContainer = new Panel { CssClass = "FormControl" };
            labelTitle = new Label { CssClass = "FormControlText" };
            help = new Label { CssClass = "FormControlHelp" };
            labelValue = new Label { CssClass = "FormControlValue" };
        }

        // Overrides 
        protected override void CreateChildControls()
        {
            divContainer.Controls.Add(labelTitle);
            divContainer.Controls.Add(help);
            divContainer.Controls.Add(labelValue);
            Controls.Add(divContainer);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            divContainer.RenderControl(writer);
        }

        public string Text
        {
            get { return labelTitle.Text; }
            set { labelTitle.Text = value; }
        }

        public string Value
        {
            get { return labelValue.Text; }
            set { labelValue.Text = value; }
        }

        public string HelpText
        {
            get { return help.Text; }
            set { help.Text = value; }
        }
    }
}