using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.UI;
using Telerik.Web.UI;

namespace Paramount.Billing.UI.Invoice
{
    public class FinalInvoice : BillingCompositeControl
    {
        public Panel invoiceHeaderPanel;
        public Image InvoiceBanner;
        public Panel issuedToPanel;
        public Panel invoiceItemsHeader;
        public Panel invoiceItems;
        public Panel paymentDetailsPanel;
        public Panel buttonsPanel;

        public Label InvoiceNumber;
        public Label InvoiceStatus;
        public Label DateOfIssue;
        public Label AccountNumber;
        public Label issuedToName;
        public Label issuedToAddress;

        public InvoiceItemsGrid invoiceItemsGrid;
        public RadGrid paymentItemsGrid;

        public Button backButton;

        public PrintPdfButton pdfButton;
        public Button printButton;
        public Button emailButton;



        #region Overrides of BillingCompositeControl

        protected override string ErrorText
        {
            get { return "Invoice is temporarily unavailable."; }
        }

        #endregion

        public FinalInvoice()
        {
            this.invoiceHeaderPanel = new Panel() { ID = "invoiceHeaderPanel" };
            this.InvoiceBanner = new Image() { ID = "invoiceBanner" };
            this.issuedToPanel = new Panel() { ID = "issuedToPanel" };
            this.invoiceItemsHeader = new Panel() { ID = "invoiceItemsHeader" };

            this.invoiceItems = new Panel() { ID = "invoiceItems" };
            this.paymentDetailsPanel = new Panel() { ID = "paymentDetailsPanel" };
            this.buttonsPanel = new Panel() { ID = "buttonsPanel" };

            this.InvoiceNumber = new Label() { ID = "InvoiceNumber" };
            this.InvoiceStatus = new Label() { ID = "InvoiceStatus" };

            this.DateOfIssue = new Label() { ID = "DateOfIssue" };
            this.AccountNumber = new Label() { ID = "AccountNumber" };
            this.issuedToName = new Label() { ID = "issuedToName" };
            this.issuedToAddress = new Label() { ID = "issuedToAddress" };
            this.backButton = new Button() { ID = "backButton" };
            this.pdfButton = new PrintPdfButton() { ID = "pdfButton" };
            this.printButton = new Button() { ID = "printButton" };
            this.emailButton = new Button() { ID = "emailButton" };

            this.invoiceItemsGrid = new InvoiceItemsGrid() {ID= "invoiceItemsGrid" };
            this.paymentItemsGrid = new RadGrid() { ID = "paymentItemsGrid" };
        }

    

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.MainPanel.Controls.Add(this.invoiceHeaderPanel);
            this.invoiceHeaderPanel.Controls.Add(this.InvoiceNumber.DivWrapLabelValue("Invoice Number"));
            this.invoiceHeaderPanel.Controls.Add(this.InvoiceStatus.DivWrapLabelValue("Invoice Status"));
            this.invoiceHeaderPanel.Controls.Add(this.DateOfIssue.DivWrapLabelValue("Date Of Issue"));
            this.invoiceHeaderPanel.Controls.Add(this.AccountNumber.DivWrapLabelValue("Account Number"));

            this.MainPanel.Controls.Add(this.InvoiceBanner);
            this.MainPanel.Controls.Add(this.issuedToPanel);

            this.issuedToPanel.Controls.Add(this.issuedToName);
            this.issuedToPanel.Controls.Add(this.issuedToAddress);

            this.MainPanel.Controls.Add(this.invoiceItemsHeader);
            this.MainPanel.Controls.Add(this.invoiceItems);
            this.invoiceItems.Controls.Add(this.invoiceItemsGrid);

            this.MainPanel.Controls.Add(this.paymentDetailsPanel);
            this.paymentDetailsPanel.Controls.Add(this.paymentItemsGrid);

            this.buttonsPanel.Controls.Add(this.backButton);
            this.buttonsPanel.Controls.Add(this.pdfButton);
            this.MainPanel.Controls.Add(this.buttonsPanel);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadData();
        }

        public InvoiceEntity InvoiceEntity { get; set; }

        public List<InvoiceItemEntity> InvoiceItemsEntity { get; set; }

        private void LoadData()
        {
            this.InvoiceEntity = this.Controller.GetInvoiceDetails(BillingSessionManager.Instance.InvoiceId);
            this.InvoiceItemsEntity = this.Controller.GetInvoiceItems(BillingSessionManager.Instance.InvoiceId);

            this.invoiceItemsGrid.LoadData(this.InvoiceItemsEntity);
            
            this.InvoiceNumber.Text = this.Controller.GetBillingSettings().ReferencePrefix + this.InvoiceEntity.InvoiceNumber.ToString().PadLeft(10,'0');
            this.InvoiceStatus.Text = this.InvoiceEntity.Status;
            this.DateOfIssue.Text = this.InvoiceEntity.DateTimeCreated.ToLongDateString();

        }
    }
}
