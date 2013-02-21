using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using Paramount.Betterclassified.Utilities.CreditCardPayment;
using Paramount.Billing.UIController;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Billing.UI.Invoice
{
    public class BankPayment : ParamountCompositeControl
    {
        private CCPaymentGatewaySettings creditCardSettings;
        private BillingSettingsEntity settings;
        private BillingSessionManager billingSessionManager;
        private InvoiceEntity invoice;

        private UIController.BillingController controller;
        protected BillingController Controller
        {
            get
            {
                return this.controller ?? (controller = new BillingController());
            }
        }

        private InvoiceEntity Invoice
        {
            get
            {
                invoice = invoice ?? Controller.GetInvoiceDetails(BillingSessionManager.InvoiceId);
                return invoice;
            }
        }
        private BillingSessionManager BillingSessionManager
        {
            get
            {
                return this.billingSessionManager ??
                       (this.billingSessionManager = BillingSessionManager.GetInstance(HttpContext.Current));
            }
        }

        private CCPaymentGatewaySettings CreditCardSettings
        {
            get { return creditCardSettings ?? (creditCardSettings = (CCPaymentGatewaySettings)ConfigurationManager.GetSection("ccPaymentGateway")); }
        }



        protected BillingSettingsEntity Settings
        {
            get { return this.settings ?? (this.settings = Controller.GetBillingSettings()); }

        }

        public BankPayment()
        {
            InformationFields = new List<string>();

            GstRate = Settings.GSTRate ?? 0;
            RefundPolicyUrl = CreditCardSettings.RefundPolicyUrl;
            VendorName = this.Settings.VendorCode;
            PaymentAlertEmail = this.Settings.PaymentAlertEmail;
            PaymentReference = this.BillingSessionManager.InvoiceId.ToString();
            GstIncluded = this.Invoice.GstIncluded.ToString();
            var membershipUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);

            if (membershipUser != null)
            {
                CustomerEmailAddress = membershipUser.Email;
            }
            else
            {
                throw new Exception("Invalid access. User must login before accessing invocie");
            }

            Cost = this.Invoice.TotalAmount;
            ReturnUrl = String.Format("{0}?id={1}&", this.CreditCardSettings.ReturnUrl, this.Invoice.InvoiceId);
            NotifyUrl =
                String.Format(
                    "{0}?sessionid={1}&id={2}&step={3}&totalCost={4}&",
                    this.CreditCardSettings.NotifyUrl,
                    HttpContext.Current.Session.SessionID,
                    this.BillingSessionManager.InvoiceId, "cc",
                    this.Invoice.TotalAmount.ToString());

            ReturnUrlText = this.Settings.ReturnLinkText;
        }
        public List<string> InformationFields { get; set; }
        public decimal GstRate { get; set; }
        public string RefundPolicyUrl { get; set; }
        public string VendorName { get; set; }
        public string PaymentAlertEmail { get; set; }
        public string PaymentReference { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string GstIncluded { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string ReturnUrlText { get; set; }
        public string HiddenFields { get; set; }
        public decimal Cost { get; set; }

        //private readonly HtmlGenericControl refundPolicyUrlInput;
        //private readonly HtmlGenericControl vendorNameInput;
        //private readonly HtmlGenericControl paymentAlertEmailInput;


        protected override void CreateChildControls()
        {
            Controls.Add(CreateHiddenfield("vendor_name", VendorName));
            Controls.Add(CreateHiddenfield("refund_policy_url", RefundPolicyUrl));
            Controls.Add(CreateHiddenfield("payment_alert", PaymentAlertEmail));
            Controls.Add(CreateHiddenfield("payment_reference", PaymentReference));
            Controls.Add(CreateHiddenfield("receipt_address", CustomerEmailAddress));
            Controls.Add(CreateHiddenfield("gst_rate", GstRate.ToString(CultureInfo.InvariantCulture)));
            Controls.Add(CreateHiddenfield("gst_added", GstIncluded));
            Controls.Add(CreateHiddenfield("return_link_url", ReturnUrl));
            Controls.Add(CreateHiddenfield("reply_link_url", NotifyUrl));
            Controls.Add(CreateHiddenfield("return_link_text", ReturnUrlText));

            //AddProduct(PaymentReference, Cost);  

            var invocieItems = this.Controller.GetInvoiceItems(this.Invoice.InvoiceId);
            foreach (var invoiceItemEntity in invocieItems)
            {
                Controls.Add(CreateHiddenfield(invoiceItemEntity.ProductType, (invoiceItemEntity.Price * invoiceItemEntity.Quantity).ToString()));
            }

            
            //Add information Fields
            foreach (string item in InformationFields)
            {
                Controls.Add(CreateHiddenfield("information_fields", item));
            }


           
        }

    

        public void AddProduct(string description, decimal value)
        {
            Controls.Add(CreateHiddenfield(description, value.ToString(CultureInfo.InvariantCulture)));
        }

        private static HtmlGenericControl CreateHiddenfield(string name, string value)
        {
            var g = new HtmlGenericControl("input");
            g.Attributes.Add("name", name);
            g.Attributes.Add("type", "hidden");
            g.Attributes.Add("value", value);
            return g;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(PaypalPayment), "bank_form_submit", string.Format(@"<script type=""text/javascript"">$(document).ready(function(){{ var form = $(""form#payForm""); form.attr(""action"", ""{0}""); form.submit();  }});</script>", this.settings.GatewayUrl));

        }

    }
}
