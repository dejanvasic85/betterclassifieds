using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.UI.HtmlControls;
using Paramount.Betterclassified.Utilities.CreditCardPayment;
using Paramount.Betterclassified.Utilities.PayPal;
using Paramount.Billing.UI.Enums;
using Paramount.Billing.UIController;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Billing.UI.Invoice
{
    public class PaypalPayment : ParamountCompositeControl
    {
        public string BusinessEmail { get; set; }
        public string ItemName { get; set; }
        public decimal Cost { get; set; }
        public string SuccessUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string CancelPurchaseUrl { get; set; }
        public string CurrencyCode { get; set; }

        private CCPaymentGatewaySettings creditCardSettings;
        private BillingSettingsEntity settings;
        private BillingSessionManager billingSessionManager;
        private InvoiceEntity invoice;
        private PayPalSettings payPalSettings;

        private UIController.BillingController controller;
        protected BillingController Controller
        {
            get
            {
                return this.controller ?? (controller = new BillingController());
            }
        }

        private PayPalSettings PayPalSettings
        {
            get { return payPalSettings ?? (payPalSettings = (PayPalSettings)ConfigurationManager.GetSection("paypal")); }
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

        public PaypalPayment()
        {
            BusinessEmail = this.Settings.PaypalBusinessEmail;
            ItemName = this.Invoice.Title;
            Cost = 12; //this.Invoice.TotalAmount;
            CancelPurchaseUrl = string.Format("{0}?step={1}&id={2}", this.PayPalSettings.CancelPurchaseUrl, BillingSteps.BillingStepCancelPurchase, this.Invoice.InvoiceId);
            CurrencyCode = this.Settings.PaypalCurrencyCode;
            SuccessUrl = String.Format("{0}?id={1}&step={2}", this.PayPalSettings.SuccessUrl, this.Invoice.InvoiceId,BillingSteps.BillingStepSuccess);
            NotifyUrl =
                String.Format(
                    "{0}?sessionid={1}&id={2}&step={3}&totalCost={4}&",
                    this.PayPalSettings.NotifyUrl,
                    HttpContext.Current.Session.SessionID,
                    this.BillingSessionManager.InvoiceId,
                    BillingSteps.BillingStepPpNotify,
                    this.Invoice.TotalAmount);
        }

        protected override void CreateChildControls()
        {
            Controls.Add(CreateHiddenfield("cmd", "_xclick"));
            Controls.Add(CreateHiddenfield("business", BusinessEmail.Trim()));
            Controls.Add(CreateHiddenfield("item_name", ItemName.Trim()));
            Controls.Add(CreateHiddenfield("amount", Cost.ToString(CultureInfo.InvariantCulture)));
            Controls.Add(CreateHiddenfield("no_shipping", "1"));
            
            Controls.Add(CreateHiddenfield("rm", "2"));

            Controls.Add(CreateHiddenfield("return", SuccessUrl.Trim()));
            Controls.Add(CreateHiddenfield("notify_url", NotifyUrl.Trim()));
            Controls.Add(CreateHiddenfield("cancel_return", CancelPurchaseUrl.Trim()));
            Controls.Add(CreateHiddenfield("currency_code", CurrencyCode.Trim()));

            Controls.Add(CreateHiddenfield("custom", Invoice.InvoiceId.ToString()));
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
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(PaypalPayment), "paypal_form_submit", string.Format(@"<script type=""text/javascript"">$(document).ready(function(){{ var form = $(""form:first""); form.attr(""action"", ""{0}""); form.submit();  }});</script>", this.payPalSettings.PayPalUrl));
            
        }
    }
}
