namespace Paramount.Broadcast.Components.UI
{
    using System.Collections.ObjectModel;
    using System.Web.UI.WebControls;
    using Common.UI.BaseControls;

    public class TemplateListControl : ParamountCompositeControl
    {
        protected DropDownList _templateListBox;
        protected Label _emailText;
        protected Collection<EmailTemplateView> _dataSource;

        public string SelectedTemplateName { get; set; }

        public TemplateListControl()
        {
            _emailText = new Label();
            _templateListBox = new DropDownList { ID = "templateListbox", AutoPostBack = true, DataTextField = "Name", DataValueField = "Id", EnableViewState = true };
            _templateListBox.SelectedIndexChanged += (TemplateListBoxSelectedIndexChanged);
        }
        
        void TemplateListBoxSelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedTemplateName = _templateListBox.SelectedValue;
            _emailText.Text = _templateListBox.SelectedValue;
        }

        protected override void CreateChildControls()
        {
            if (!this.Page.IsPostBack)
            {
                //_templateListBox.DataSource = EmailBroadcastController.GetTemplatesForEntity(ConfigSettingReader.EntityCode);

                _templateListBox.DataBind();
            }
            var panel = new Panel();
            panel.Controls.Add(_templateListBox);
            panel.Controls.Add(_emailText);
            Controls.Add(panel);
        }
    }
}
