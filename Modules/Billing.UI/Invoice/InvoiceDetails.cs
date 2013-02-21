using System;
using System.Web;
using System.Web.UI.WebControls;
using Paramount.Billing.UI.Enums;
using Paramount.Common.DataTransferObjects.Enums;
using Telerik.Web.UI;
using Paramount.Common.DataTransferObjects.Billing;

namespace Paramount.Billing.UI.Invoice
{
    public class InvoiceDetails: BillingWizardStep
    {
        protected Panel controlsPanel;
        protected Panel headerPanel;
        protected Panel addressPanel;
        protected Panel itemsPanel;
        protected Panel paymentOptionsPanel;
        protected Panel buttonsPanel;

        protected Label header;
        protected AddressView billingAddress;
        protected AddressView deliveryAddress;
        protected InvoiceItemsGrid invoiceItems;
        protected PaymentOptions paymentOptions;

        protected override bool Save()
        {
            
            BillingSessionManager.GetInstance(HttpContext.Current).PaymentType = this.paymentOptions.OptionId;
            if (this.Controller.ConfirmInvoice(BillingSessionManager.Instance.InvoiceId, this.paymentOptions.OptionId))
            {
                GoTo(BillingSteps.BillingStepPayment);
            }
            return true;
        }

        protected override string CancelButtonText { get { return "Back"; } }

        protected override  string SaveButtonText { get { return "Pay Now"; } }
        
        public InvoiceDetails()
        {
            this.controlsPanel = new Panel() { ID = "controlsPanel" };
            this.MainPanel.Controls.Add(this.controlsPanel);
            this.headerPanel = new Panel() { ID = "headerPanel" };
            this.addressPanel = new Panel() { ID = "addressPanel" };
            this.itemsPanel = new Panel() { ID = "itemsPanel" };
            this.paymentOptionsPanel = new Panel() { ID = "paymentOptionsPanel" };
            this.buttonsPanel = new Panel() { ID = "buttonsPanel" };
            
            this.header = new Label() { ID = "header" };

            this.billingAddress = new AddressView() { ID = "billingAddress", TitleText = "Billing Address" };
            this.deliveryAddress = new AddressView() { ID = "deliveryAddress", TitleText = "Delivery Address" };
            this.invoiceItems = new InvoiceItemsGrid() { ID = "invoiceItems" };
            this.paymentOptions = new PaymentOptions() { ID = "paymentOptions" };

        }

        protected override bool Cancel()
        {
            GoTo(BillingSteps.BillingStepAddress);
            return true;
        }
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.MainPanel.Controls.Add(this.controlsPanel);
            this.controlsPanel.Controls.Add(this.headerPanel);
            header.Text = "Please confirm the following details:";
            this.headerPanel.Controls.Add(this.header);
                
            this.controlsPanel.Controls.Add(this.addressPanel);
            this.addressPanel.Controls.Add(this.billingAddress);
            this.addressPanel.Controls.Add(this.deliveryAddress);

            this.controlsPanel.Controls.Add(this.itemsPanel);
            this.itemsPanel.Controls.Add(this.invoiceItems);

            this.controlsPanel.Controls.Add(this.paymentOptionsPanel);
            this.paymentOptionsPanel.Controls.Add(this.paymentOptions);
        }

        protected override string ErrorText
        {
            get { return "Paramount shopping invoice is temporarily unavialable"; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadControls();
        }

        private void LoadControls()
        {
            this.invoiceItems.LoadData(this.Controller.GetInvoiceItems(BillingSessionManager.Instance.InvoiceId));
            

            var paymentType = this.Controller.GetInvoiceDetails(BillingSessionManager.Instance.InvoiceId).PaymentType;
            if (!string.IsNullOrEmpty(paymentType))
            {
                this.paymentOptions.OptionId = (PaymentType)Enum.Parse(typeof(PaymentType), paymentType);
            }


        }
    }
}
