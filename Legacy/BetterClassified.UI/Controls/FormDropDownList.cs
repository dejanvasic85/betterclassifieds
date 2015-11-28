using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BetterClassified.UI
{
    public class FormDropDownList : CompositeControl
    {
        // Inner controls
        private readonly HtmlGenericControl parentContainer;
        private readonly HtmlGenericControl label;
        private readonly HtmlGenericControl help;
        private readonly HtmlGenericControl controlContainer;
        private readonly RadComboBox comboBox;

        // Events
        public event SelectedIndexChanged IndexChanged;

        public delegate void SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs eventArgs);

        // Constructor
        public FormDropDownList()
        {
            parentContainer = new HtmlGenericControl("div");
            parentContainer.Attributes.Add("class", "formcontrol-container");

            controlContainer = new HtmlGenericControl("div");
            controlContainer.Attributes.Add("class", "control");

            comboBox = new RadComboBox { AutoPostBack = true };
            comboBox.SelectedIndexChanged += ComboBoxOnSelectedIndexChanged;

            label = new HtmlGenericControl("label");
            
            help = new HtmlGenericControl("label");
            help.Attributes.Add("class", "helptext");
            
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
            Controls.Clear();

            parentContainer.Controls.Add(label);
            parentContainer.Controls.Add(help);
            controlContainer.Controls.Add(comboBox);
            parentContainer.Controls.Add(controlContainer);
            
            Controls.Add(parentContainer);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            parentContainer.RenderControl(writer);
        }

        // Public properties
        public string Text
        {
            get { return label.InnerText; }
            set { label.InnerText = value; }
        }

        public string HelpText
        {
            get { return help.InnerText; }
            set { help.InnerText = value; }
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

        public new Unit Width
        {
            get { return this.comboBox.Width; }
            set { this.comboBox.Width = value; }
        }
    }
}
