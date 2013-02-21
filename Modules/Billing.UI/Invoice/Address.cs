using System;
using System.Web.UI.WebControls;
using Paramount.Billing.UI.Enums;
using Paramount.Billing.UI.ShoppingCart;
using Paramount.Common.DataTransferObjects.Billing;

namespace Paramount.Billing.UI.Invoice
{
    public class Address : BillingWizardStep
    {
        public AddressControl billingAddress;
        public AddressControl deliveryAddress;
        public CheckBox SameAsAbove;
        public Panel controlsPanel;
        
        public Address()
        {
            if (BillingSessionManager.Instance.InvoiceId == Guid.Empty)
            {
                throw new Exception("Invoice is not created yet.");
            }
            this.controlsPanel = new Panel() { ID = "mainPanel" };
            this.billingAddress = new AddressControl() { ID = "billingAddress" };
            this.SameAsAbove = new CheckBox() { ID = "SameAsAbove", AutoPostBack = true};
            this.deliveryAddress = new AddressControl() { ID = "deliveryAddress" };

            this.SameAsAbove.CheckedChanged += SameAsAboveChecked;
        }

        private void SameAsAboveChecked(object sender, EventArgs e)
        {
            if (this.SameAsAbove.Checked)
            {
                this.billingAddress.Address = this.deliveryAddress.Address;
                
            }
            this.billingAddress.Enabled = !this.SameAsAbove.Checked;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.MainPanel.Controls.Add(this.controlsPanel);
            this.controlsPanel.Controls.Add(new Label() { Text = "Delivery Address" });
            this.controlsPanel.Controls.Add(this.deliveryAddress);
            this.controlsPanel.Controls.Add(this.SameAsAbove);
            this.controlsPanel.Controls.Add(new Label() { Text = "Billing Address" });
            this.controlsPanel.Controls.Add(this.billingAddress);
        }

        protected override bool Cancel()
        {
            GoPrevious();
            return true;
        }

        protected override bool Save()
        {
            if (!Page.IsValid) return false;
            if  (Controller.UpdateInvoiceAddress(BillingSessionManager.Instance.InvoiceId, this.billingAddress.Address, this.deliveryAddress.Address))
            {
                GoTo(BillingSteps.BillingStepPaymentOption);
            }
            return false;
        }

        protected override string ErrorText
        {
            get { return "Address details are temporarily unavailable"; }
        }

        public AddressDetails BillingAddress
        {
            get { return this.billingAddress.Address; }
            set { this.billingAddress.Address = value; }
        }

        public AddressDetails DeliveryAddress
        {
            get { return this.deliveryAddress.Address; }
            set { this.deliveryAddress.Address = value; }
        }
    }
}
