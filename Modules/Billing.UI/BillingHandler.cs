using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Paramount.Betterclassified.Utilities.CreditCardPayment;
using Paramount.Betterclassified.Utilities.PayPal;
using Paramount.Billing.UI.Enums;
using Paramount.Billing.UI.Invoice;
using Paramount.Billing.UIController;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.DataTransferObjects.Enums;
using Paramount.Modules.Logging.UIController;

namespace Paramount.Billing.UI
{
    public class BillingHandler : IHttpHandler, IRequiresSessionState
    {
        private BillingSessionManager billingSessionManager;
        BillingSettingsEntity settings;
        private HttpContext currentContext;
        private UIController.BillingController controller;
        private CCPaymentGatewaySettings creditCardSettings;
        private PayPalSettings payPalSettings;
        private InvoiceEntity invoice;

        protected BillingController Controller
        {
            get
            {
                return this.controller ?? (controller = new BillingController());
            }
        }

        #region Implementation of IHttpHandler

        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            this.billingSessionManager = BillingSessionManager.GetInstance(currentContext);
            settings = Controller.GetBillingSettings();
            var step = currentContext.Request.QueryString["step"];

            var billingStep = BillingSteps.BillingStepAddress;
            if (!string.IsNullOrEmpty(step))
            {
                billingStep = (BillingSteps)Enum.Parse(typeof(BillingSteps), step);
            }


            switch (billingStep)
            {
                case BillingSteps.BillingStepAddress:
                    GoToAddress(true);
                    break;
                case BillingSteps.BillingStepPaymentOption:
                    GoToPaymentOption();
                    break;
                case BillingSteps.BillingStepPayment:
                    GoToPayment();
                    break;
                case BillingSteps.BillingStepFinalInvoice:
                    GoToFinalInvoice();
                    break;
                case BillingSteps.BillingStepSuccess:
                    Success();
                    break;
                case BillingSteps.BillingStepPaid:
                    GoToFinalInvoice();
                    break;
                case BillingSteps.BillingStepCcNotify:
                    CreditCardNotify();
                    break;
                case BillingSteps.BillingStepPpNotify:
                    PayPalNotify();
                    break;
                case BillingSteps.BillingStepFail:
                    Fail();
                    break;
                case BillingSteps.BillingStepCancelPurchase:
                    CancelPurchase();
                    break;
                default:
                    GotoSameStep();
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        private void GotoPreviousStep()
        {
            switch (billingSessionManager.BillingCurrentStep)
            {
                case BillingSteps.BillingStepAddress:
                    ReturnToCaller();
                    break;

                case BillingSteps.BillingStepPaymentOption:
                    GoToAddress(false);
                    break;

                case BillingSteps.BillingStepPayment:
                    GoToPaymentOption();
                    break;
            }
        }

        private void GoToDefault()
        {
            billingSessionManager.ReturnUrl = currentContext.Request.Url.ToString();
            GoToAddress(true);
        }

        private void GoToAddress(bool forward)
        {
            if (settings.CollectAddressDetails)
            {
                billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepAddress;
                currentContext.Response.Redirect(ApplicationBlock.Configuration.ConfigSettingReader.BillingStepAddressUrl);
            }
            else
            {
                if (forward)
                {
                    GoToPaymentOption();
                }
                else
                {
                    ReturnToCaller();
                }
            }

        }

        private void ReturnToCaller()
        {
            currentContext.Response.Redirect(billingSessionManager.ReturnUrl);
        }

        private void GotoNextStep()
        {
            switch (billingSessionManager.BillingCurrentStep)
            {
                case BillingSteps.BillingStepAddress:
                    GoToPaymentOption();
                    break;

                case BillingSteps.BillingStepPaymentOption:
                    GoToPayment();
                    break;

                case BillingSteps.BillingStepFinalInvoice:
                    ReturnToCaller();
                    break;

                default:
                    GoToDefault();
                    break;
            }
        }

        private void GotoSameStep()
        {
            switch (billingSessionManager.BillingCurrentStep)
            {
                case BillingSteps.BillingStepAddress:
                    GoToAddress(true);
                    break;

                case BillingSteps.BillingStepPaymentOption:
                    GoToPaymentOption();
                    break;

                case BillingSteps.BillingStepPayment:
                    GoToPayment();
                    break;

                case BillingSteps.BillingStepFinalInvoice:
                    GoToFinalInvoice();
                    break;

                default:
                    GoToDefault();
                    break;
            }
        }

        private void GoToFinalInvoice()
        {
            billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepFinalInvoice;
            currentContext.Response.Redirect(ApplicationBlock.Configuration.ConfigSettingReader.BillingStepFinalInvoiceUrl);
        }

        private void CancelPurchase()
        {
            billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepCancelPurchase;
            currentContext.Response.Redirect(ApplicationBlock.Configuration.ConfigSettingReader.BillingStepCancelPurchase);
        }

        private void GoToPayment()
        {

            switch (BillingSessionManager.GetInstance(currentContext).PaymentType)
            {
                case PaymentType.None:
                    return;
                case PaymentType.Bank:
                    billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepPayment;
                    currentContext.Response.Redirect(ApplicationBlock.Configuration.ConfigSettingReader.BillingStepBankPayment);
                    //Utility.GetControlString.RenderControl(new BankPayment()
                    //                                           {
                    //                                               GstRate = settings.GSTRate ?? 0,
                    //                                               RefundPolicyUrl =
                    //                                                   this.CreditCardSettings.RefundPolicyUrl,
                    //                                               VendorName = this.settings.VendorCode,
                    //                                               PaymentAlertEmail =
                    //                                                   this.settings.PaymentAlertEmail,
                    //                                               PaymentReference =
                    //                                                   this.billingSessionManager.InvoiceId.ToString(),
                    //                                               GstIncluded = this.Invoice.GstIncluded.ToString(),

                    //                                               Cost = this.Invoice.TotalAmount,
                    //                                               ReturnUrl = String.Format("{0}?id={1}&", this.creditCardSettings.ReturnUrl, this.Invoice.InvoiceId),
                    //                                               NotifyUrl =
                    //                                                   String.Format(
                    //                                                       "{0}?sessionid={1}&id={2}&step={3}&totalCost={4}&",
                    //                                                       this.CreditCardSettings.NotifyUrl,
                    //                                                       HttpContext.Current.Session.SessionID,
                    //                                                       this.billingSessionManager.InvoiceId, "cc",
                    //                                                       this.Invoice.TotalAmount.ToString()),

                    //                                               ReturnUrlText = this.settings.ReturnLinkText,
                    //                                           });


                    break;
                case PaymentType.Paypal:
                    billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepPayment;
                    currentContext.Response.Redirect(ApplicationBlock.Configuration.ConfigSettingReader.BillingStepPayPalPayment);
                    //Utility.GetControlString.RenderControl(new PaypalPayment()
                    //                                           {
                    //                                               BusinessEmail = this.settings.PaypalBusinessEmail,
                    //                                               ItemName = this.Invoice.Title,
                    //                                               Cost = this.Invoice.TotalAmount,
                    //                                               CancelPurchaseUrl = this.PayPalSettings.CancelPurchaseUrl,
                    //                                               CurrencyCode = this.settings.PaypalCurrencyCode,
                    //                                               SuccessUrl = String.Format("{0}?id={1}&", this.payPalSettings.SuccessUrl, this.Invoice.InvoiceId),
                    //                                               NotifyUrl =
                    //                                                   String.Format(
                    //                                                       "{0}?sessionid={1}&id={2}&step={3}&totalCost={4}&",
                    //                                                       this.CreditCardSettings.NotifyUrl,
                    //                                                       HttpContext.Current.Session.SessionID,
                    //                                                       this.billingSessionManager.InvoiceId, "cc",
                    //                                                       this.Invoice.TotalAmount.ToString()),
                    //                                           }, true);


                    break;
            }

            //currentContext.Response.Redirect(settings.GatewayUrl);
        }

        private InvoiceEntity Invoice
        {
            get
            {
                invoice = invoice ?? Controller.GetInvoiceDetails(billingSessionManager.InvoiceId);
                return invoice;
            }
        }

        private void GoToPaymentOption()
        {
            billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepPaymentOption;
            currentContext.Response.Redirect(ApplicationBlock.Configuration.ConfigSettingReader.BillingStepPaymentOptionUrl);
        }

        private static string Encode(string oldValue)
        {
            var newValue = oldValue.Replace("\"", "'");
            newValue = System.Web.HttpUtility.UrlEncode(newValue);
            newValue = newValue.Replace("%2f", "/");
            return newValue;
        }

        private PayPalSettings PayPalSettings
        {
            get { return payPalSettings ?? (payPalSettings = (PayPalSettings)ConfigurationManager.GetSection("paypal")); }
        }

        private CCPaymentGatewaySettings CreditCardSettings
        {
            get { return creditCardSettings ?? (creditCardSettings = (CCPaymentGatewaySettings)ConfigurationManager.GetSection("ccPaymentGateway")); }
        }

        private bool CreditCardNotify()
        {
            if (this.Invoice != null)
            {
                //'redirect to book successful page
                //    Global_asax.OnPayment.BeginInvoke(ref, Nothing, Nothing)

                //todo: how to find out if the payment is paid
                Finalise();
            }
            return true;
        }

        private void Finalise()
        {
            var invoiceId = (this.currentContext.Request["id"]);
            var sessionId = (this.currentContext.Request["sessionId"]);
            var totalAmountString = (this.currentContext.Request["totalCost"]);
            decimal totalAmount;
            if (!decimal.TryParse(totalAmountString,out totalAmount))
            {
                return;
            }
            
           if (this.Controller.MarkInvoiceAsPaid(new Guid(invoiceId), sessionId, totalAmount))
           {
               var invoiceItems = this.Controller.GetInvoiceItems(new Guid(invoiceId));
               foreach (var item in invoiceItems)
               {
                    ((IBillingPayment)this.currentContext.ApplicationInstance).InvoicePaid(item.ReferenceId, item.Price, item.ProductType);
               }
           }
           GoToFinalInvoice();
        }

        private string ReferenceId
        {
            get { return this.currentContext.Request["id"]; }
        }

        private bool PayPalNotify()
        {
            try
            {
                // Step 1a: Modify the POST string.
                var formPostData = "cmd = _notify-validate";
                foreach (var postKey in currentContext.Request.Form.AllKeys)
                {
                    var postValue = Encode(currentContext.Request.Form[postKey]);
                    formPostData += string.Format("&{0}={1}", postKey, postValue);
                }

                var client = new WebClient();

                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var postByteArray = Encoding.ASCII.GetBytes(formPostData);
                var responseArray = client.UploadData(PayPalSettings.PayPalUrl, "POST", postByteArray);
                var response = Encoding.ASCII.GetString(responseArray);

                switch (response)
                {
                    case "VERIFIED":
                        if (this.Invoice != null)
                        {
                            Finalise();
                        }
                        break;

                    default:
                        //  Possible fraud.Log for investigation.
                        var membershipUser = Membership.GetUser();
                        var userName = string.Empty;
                        if (membershipUser != null)
                        {
                            userName = membershipUser.UserName;
                        }
                        var secError = string.Format("Possible Fraud occurance using paypal system.{0}Paypal User information{1}-----------------------{2}Username: {3}{4}First Name: {5}Last Name: {6}{7}{8}Betterclassifieds User Information {9}---------------------------------- {10}Username: {11}", Environment.NewLine, Environment.NewLine, Environment.NewLine, this.currentContext.Request["payer_email"], Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, userName);
                        throw new Exception(secError);



                }
            }
            catch (Exception ex)
            {
                ExceptionLogController<Exception>.AuditException(ex);
                return false;
            }
            return true;
        }

        private void Success()
        {
            billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepSuccess;
            currentContext.Response.Redirect(string.Format("{0}?id={1}", ApplicationBlock.Configuration.ConfigSettingReader.BillingStepSuccessWaiting, this.ReferenceId));
        }

        private void Fail()
        {
            billingSessionManager.BillingCurrentStep = BillingSteps.BillingStepFail;
            currentContext.Response.Redirect(string.Format("{0}?action=fail", ApplicationBlock.Configuration.ConfigSettingReader.BillingStepFailed));
        }
    }
}
