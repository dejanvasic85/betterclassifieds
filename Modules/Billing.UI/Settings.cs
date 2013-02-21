using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Paramount.Billing.UIController;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.UI.BaseControls;
using Paramount.DSL.UI;
using Paramount.Common.UI;

namespace Paramount.Billing.UI
{
    public class Settings : BillingCompositeUpdateControl
    {
        protected Panel controlsPanel;
        protected Panel generalPanel;
        protected Panel payPalPanel;
        protected Panel bankPanel;

        protected CheckBox collectAddressDetails;
        protected ImageUploadControl invoiceBanner;
        protected TextBox referenceText;
        protected RequiredFieldValidator requiredReferenceText;

        protected TextBox businessEmail;
        protected RequiredFieldValidator requiredBusinessEmail;
        private readonly RegularExpressionValidator validBusinessEmail;
        protected DropDownList currencyCode;
        protected RequiredFieldValidator requiredCurrencyCode;

        protected DropDownList bank;
        protected RequiredFieldValidator requiredBank;

        protected TextBox gatewayUrl;
        protected RequiredFieldValidator requiredGatewayUrl;
        private readonly RegularExpressionValidator validGatewayUrl;
        protected TextBox returnLinkText;
        protected RequiredFieldValidator requiredReturnLinkText;
        protected CheckBox gstIncluded;
        protected TextBox gstRate;
        protected RequiredFieldValidator requiredGstRate;
        protected FilteredTextBoxExtender gstRateFilter;
        protected TextBox paymentAlertEmail;
        protected RequiredFieldValidator requiredPaymentAlertEmail; 
        private readonly RegularExpressionValidator validPaymentAlertEmail;
        protected TextBox vendorName;
        protected RequiredFieldValidator requiredVendorName;

        protected ValidationSummary validationSummary;
        //var a = new AjaxControlToolkit.FilteredTextBoxExtender {FilterType = FilterTypes.Numbers};
        //var t = new TextBox();

        //a.TargetControlID = t.ClientID;

        public Settings()
        {
            this.controlsPanel = new Panel() { ID = "mainPanel" };
            this.generalPanel = new Panel() { ID = "generalPanel" };
            this.payPalPanel = new Panel() { ID = "payPalPanel" };
            this.bankPanel = new Panel() { ID = "bankPanel" };
            this.collectAddressDetails = new CheckBox() { ID = "collectAddressDetails" };
            this.invoiceBanner = new ImageUploadControl() { ID = "invoiceBanner" };

            this.referenceText = new TextBox() { ID = "referenceText" };
            this.requiredReferenceText = new RequiredFieldValidator() { ID = "requiredReferenceText", ControlToValidate = "referenceText", ErrorMessage = "Reference Prefix is a required field", Text = "&nbsp;" };

            this.businessEmail = new TextBox() { ID = "businessEmail" };
            this.validBusinessEmail = new RegularExpressionValidator() { ID = "validBusinessEmail", ControlToValidate = "businessEmail", ErrorMessage = "Invalid Email address for Business Email", Text = "&nbsp;", ValidationExpression = Common.UI.Constants.ValidationExpressions.Email };
            this.requiredBusinessEmail = new RequiredFieldValidator() { ID = "requiredBusinessEmail", ControlToValidate = "businessEmail", ErrorMessage = "Business email is a required field", Text = "&nbsp;" };

            this.currencyCode = new DropDownList() { ID = "currencyCode", EnableViewState = true };
            this.requiredCurrencyCode = new RequiredFieldValidator() { ID = "requiredCurrencyCode", ControlToValidate = "currencyCode", ErrorMessage = "Currency Code is a required field", Text = "&nbsp;", InitialValue = string.Empty };

            this.bank = new DropDownList() { ID = "bank", AutoPostBack = true };
            this.requiredBank = new RequiredFieldValidator() { ID = "requiredBank", ControlToValidate = "bank", ErrorMessage = "Bank is a required field", Text = "&nbsp;", InitialValue = string.Empty };
            this.bank.SelectedIndexChanged += BankSelected;

            this.gatewayUrl = new TextBox() { ID = "gatewayUrl" };
            this.validGatewayUrl = new RegularExpressionValidator() { ID = "validGatewayUrl", ControlToValidate = "gatewayUrl", ErrorMessage = "Invalid URL for Gateway", Text = "&nbsp;", ValidationExpression = Common.UI.Constants.ValidationExpressions.Url };
            this.requiredGatewayUrl = new RequiredFieldValidator() { ID = "requiredGatewayUrl", ControlToValidate = "gatewayUrl", ErrorMessage = "Gateway Url is a required field", Text = "&nbsp;" };

            this.returnLinkText = new TextBox() { ID = "returnLinkText" };
            this.requiredReturnLinkText = new RequiredFieldValidator() { ID = "requiredReturnLinkText", ControlToValidate = "returnLinkText", ErrorMessage = "Return Link Text is a required field", Text = "&nbsp;" };

            this.gstIncluded = new CheckBox() { ID = "gstIncluded" };

            this.gstRate = new TextBox() { ID = "gstRate" };
            this.requiredGstRate = new RequiredFieldValidator() { ID = "requiredGstRate", ControlToValidate = "gstRate", ErrorMessage = "GST rate is a required field", Text = "&nbsp;" };
            gstRateFilter = new FilteredTextBoxExtender { FilterType = FilterTypes.Numbers, FilterMode = FilterModes.InvalidChars, TargetControlID = gstRate.ClientID };

            //paymentAlertEmail
            this.paymentAlertEmail = new TextBox() { ID = "paymentAlertEmail" };
            this.validPaymentAlertEmail = new RegularExpressionValidator() { ID = "validPaymentAlertEmail", ControlToValidate = "paymentAlertEmail", ErrorMessage = "Invalid Email address for Payment Alert Email", Text = "&nbsp;", ValidationExpression = Common.UI.Constants.ValidationExpressions.Email };
            this.requiredPaymentAlertEmail = new RequiredFieldValidator() { ID = "requiredPaymentAlertEmail", ControlToValidate = "paymentAlertEmail", ErrorMessage = "Payment Alert email is a required field", Text = "&nbsp;" };


            this.vendorName = new TextBox() { ID = "vendorName" };
            this.requiredVendorName = new RequiredFieldValidator() { ID = "requiredVendorName", ControlToValidate = "vendorName", ErrorMessage = "Vendor is a required field", Text = "&nbsp;" };



            this.validationSummary = new ValidationSummary() { ID = "validationSummary" };
        }

        private void BankSelected(object sender, EventArgs e)
        {
            var bankDetails =
                this.Controller.GetBankList().AsQueryable().FirstOrDefault(
                    a => a.BankId.ToString() == this.bank.SelectedValue);

            if (bankDetails == null)
            {
                return;
            }

            this.gatewayUrl.Text = bankDetails.GatewayUrl;
            this.returnLinkText.Text = bankDetails.ReturnLinkText;
            this.gstRate.Text = bankDetails.GSTRate.ToString();

        }

        protected override bool Cancel()
        {
            return true;
        }

        protected override bool Save()
        {
            var entity = this.SettingsEntity;
            if (invoiceBanner.Upload())
            {
                entity.InvoiceBannerImageId = new Guid(invoiceBanner.DocumentId);
            }
            return this.Controller.SaveBillingSettings(entity);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            var generalDetailsTitle = new Label { Text = "General Details" };
            this.MainPanel.Controls.Add(this.controlsPanel);
            this.controlsPanel.Controls.Add(this.generalPanel);
            this.generalPanel.Controls.Add(generalDetailsTitle);
            this.generalPanel.Controls.Add(this.collectAddressDetails.DivWrapLabelValue("Collect Address Details"));
            this.generalPanel.Controls.Add(this.invoiceBanner.DivWrapLabelValue("Invoice Banner"));
            this.generalPanel.Controls.Add(this.referenceText.DivWrapLabelValue("Reference Prefix", this.requiredReferenceText));

            this.controlsPanel.Controls.Add(this.payPalPanel);
            this.payPalPanel.Controls.Add(new Label { Text = "Pay Pal" });
            this.payPalPanel.Controls.Add(this.businessEmail.DivWrapLabelValue("Business Email", new List<Control>() { this.requiredBusinessEmail, this.validBusinessEmail }));
            this.payPalPanel.Controls.Add(this.currencyCode.DivWrapLabelValue("Currency", this.requiredCurrencyCode));

            this.controlsPanel.Controls.Add(this.bankPanel);
            this.bankPanel.Controls.Add(new Label { Text = "Bank" });
            this.bankPanel.Controls.Add(this.bank.DivWrapLabelValue("Bank", this.requiredBank));
            this.bankPanel.Controls.Add(this.gatewayUrl.DivWrapLabelValue("Gateway URL", new List<Control>() { this.validGatewayUrl, this.requiredGatewayUrl }));
            this.bankPanel.Controls.Add(this.returnLinkText.DivWrapLabelValue("Return Link Text", this.requiredReturnLinkText));
            this.bankPanel.Controls.Add(this.gstIncluded.DivWrapLabelValue("GST Included"));
            this.bankPanel.Controls.Add(this.gstRate.DivWrapLabelValue("GST Rate", this.requiredGstRate));
            this.payPalPanel.Controls.Add(this.paymentAlertEmail.DivWrapLabelValue("Payment Alert", new List<Control>() { this.requiredPaymentAlertEmail, this.validPaymentAlertEmail }));
            this.buttonPanel.Controls.Add(this.gstRateFilter);
            this.bankPanel.Controls.Add(this.vendorName.DivWrapLabelValue("Vendor Name", this.requiredVendorName));

            this.controlsPanel.Controls.Add(this.validationSummary);
        }

        protected override string ErrorText
        {
            get { return "Settings are temporarily unavailable"; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            //if (this.Page.IsPostBack)
            //{
            //    return;
            //}

            this.currencyCode.DataValueField = "CurrencyCode";
            this.currencyCode.DataTextField = "CurrencyName";
            this.currencyCode.DataSource = this.Controller.GetCurrencyList();
            this.currencyCode.DataBind();
            this.currencyCode.Items.Insert(0, new ListItem("<-- please select -->"));


            this.bank.DataValueField = "BankId";
            this.bank.DataTextField = "BankName";
            this.bank.DataSource = this.Controller.GetBankList();
            this.bank.DataBind();
            this.bank.Items.Insert(0, new ListItem("<-- please select -->"));

            this.SettingsEntity = this.Controller.GetBillingSettings();
        }

        private BillingSettingsEntity SettingsEntity
        {
            get
            {

                var bankId = string.IsNullOrEmpty(this.bank.SelectedValue)
                    ? Guid.Empty : new Guid(this.bank.SelectedValue);

                var bankName = this.bank.SelectedItem == null
                    ? string.Empty : this.bank.SelectedItem.Text;

                var paypalCurrencyCode = this.currencyCode.SelectedValue;



                return new BillingSettingsEntity()
                           {
                               CollectAddressDetails = this.collectAddressDetails.Checked,
                               InvoiceBannerImageId = (!string.IsNullOrEmpty(this.invoiceBanner.DocumentId)) ? new Guid(this.invoiceBanner.DocumentId) : Guid.Empty,
                               ReferencePrefix = this.referenceText.Text,

                               PaypalBusinessEmail = this.businessEmail.Text,
                               PaypalCurrencyCode = paypalCurrencyCode,
                               BankName = bankName,

                               GatewayUrl = this.gatewayUrl.Text,
                               ReturnLinkText = this.returnLinkText.Text,
                               GstIncluded = this.gstIncluded.Text,
                               GSTRate = (string.IsNullOrEmpty(this.gstRate.Text)) ? 0 : decimal.Parse(this.gstRate.Text),
                               PaymentAlertEmail = this.paymentAlertEmail.Text,
                               VendorCode = vendorName.Text,
                               BankId = bankId,
                           };
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                this.collectAddressDetails.Checked = value.CollectAddressDetails;
                this.invoiceBanner.DocumentId = value.InvoiceBannerImageId.ToString();
                this.referenceText.Text = value.ReferencePrefix;
                this.businessEmail.Text = value.PaypalBusinessEmail;
                this.currencyCode.Text = value.PaypalCurrencyCode.ToString();
                this.bank.Text = value.BankName;
                this.gatewayUrl.Text = value.GatewayUrl;
                this.returnLinkText.Text = value.ReturnLinkText;
                this.gstRate.Text = value.GSTRate.ToString();
                var item = this.bank.Items.FindByValue(value.BankId.ToString());
                if (item != null)
                {
                    this.bank.ClearSelection();
                    item.Selected = true;
                }
                //this.vendorName.Text=value.


            }
        }
    }
}
