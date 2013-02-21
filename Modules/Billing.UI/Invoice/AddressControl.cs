using System.Web.UI.WebControls;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.UI.BaseControls;
using Paramount.Common.UI;

namespace Paramount.Billing.UI.Invoice
{
    public class AddressControl : ParamountCompositeControl
    {
        protected Panel mainPanel;
        protected TextBox contactName;
        protected TextBox address1;
        protected TextBox address2;
        protected TextBox city;
        protected TextBox state;
        protected TextBox postCode;
        protected TextBox country;

        public AddressControl()
        {
            contactName = new TextBox() { ID = "contactName", MaxLength = 150, CssClass = "full" };
            address1 = new TextBox() { ID = "address1", MaxLength = 250, CssClass = "full" };
            address2 = new TextBox() { ID = "address2", MaxLength = 250, CssClass = "full" };
            city = new TextBox() { ID = "city", CssClass = "full" };
            state = new TextBox() { ID = "state", MaxLength = 50, CssClass = "full" };
            postCode = new TextBox() { ID = "postCode", MaxLength = 12, CssClass = "full" };
            country = new TextBox() { ID = "country", MaxLength = 250, CssClass = "full" };
            mainPanel = new Panel() { ID = "mainPanel" };
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(this.mainPanel);

            this.mainPanel.Controls.Add(contactName.DivWrapLabelValue("Contact Name"));
            this.mainPanel.Controls.Add(address1.DivWrapLabelValue("address1"));
            this.mainPanel.Controls.Add(address2.DivWrapLabelValue("address2"));
            this.mainPanel.Controls.Add(city.DivWrapLabelValue("City"));
            this.mainPanel.Controls.Add(state.DivWrapLabelValue("State"));
            this.mainPanel.Controls.Add(postCode.DivWrapLabelValue("Postcode"));
            this.mainPanel.Controls.Add(country.DivWrapLabelValue("Country"));
        }

        public AddressDetails Address
        {
            get
            {
                return new AddressDetails()
                           {
                               Address1 = this.address1.Text,
                               Address2 = this.address2.Text,
                               Country = this.country.Text,
                               Name = this.contactName.Text,
                               Postcode = this.postCode.Text,
                               State = this.state.Text,
                               City = this.city.Text,
                           };
            }
            set
            {
                this.address1.Text = value.Address1;
                this.address2.Text = value.Address2;
                this.country.Text = value.Country;
                this.contactName.Text = value.Name;
                this.postCode.Text = value.Postcode;
                this.state.Text = value.State;
                this.city.Text=value.City;
            }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                this.address1.Enabled = value;
                this.address2.Enabled = value;
                this.country.Enabled = value;
                this.contactName.Enabled = value;
                this.postCode.Enabled = value;
                this.state.Enabled = value;
                this.city.Enabled = value; 
            }
        }

    }
}
