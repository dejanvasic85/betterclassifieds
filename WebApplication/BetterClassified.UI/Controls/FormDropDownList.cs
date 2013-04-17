using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BetterClassified.UI
{
    public class FormDropDownList : CompositeControl
    {
        // Inner controls
        private readonly Panel divContainer;
        private readonly Label label;
        private readonly Label help;
        private readonly RadComboBox comboBox;

        // Events
        public event SelectedIndexChanged IndexChanged;

        public delegate void SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs eventArgs);

        // Constructor
        public FormDropDownList()
        {
            divContainer = new Panel { CssClass = "FormControl" };
            comboBox = new RadComboBox { AutoPostBack = true };
            comboBox.SelectedIndexChanged += ComboBoxOnSelectedIndexChanged;
            label = new Label { CssClass = "FormControlText" };
            help = new Label { CssClass = "FormControlHelp" };
        }

        // Bubbled events
        private void ComboBoxOnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs eventArgs)
        {
            if (this.IndexChanged != null)
                this.IndexChanged(sender, eventArgs);
        }

        // Overrides
        protected override void CreateChildControls()
        {
            divContainer.Controls.Add(label);
            divContainer.Controls.Add(help);
            divContainer.Controls.Add(comboBox);
            Controls.Add(divContainer);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            divContainer.RenderControl(writer);
        }

        // Public properties
        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public string HelpText
        {
            get { return help.Text; }
            set { help.Text = value; }
        }

        public bool IsRequired { get; set; }

        public object DataSource
        {
            get { return comboBox.DataSource; }
            set { comboBox.DataSource = value; }
        }

        public string SelectedValue
        {
            get { return this.comboBox.SelectedValue; }
            set { this.comboBox.SelectedValue = value; }
        }
    }
}
