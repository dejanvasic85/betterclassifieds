using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.Billing.UI.Enums;
using Paramount.Billing.UIController;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.DataTransferObjects.Enums;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Billing.UI.Invoice
{
    public class SuccessWait : ParamountCompositeControl
    {
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

       


        protected BillingSettingsEntity Settings
        {
            get { return this.settings ?? (this.settings = Controller.GetBillingSettings()); }

        }
        public string AspxPageName { get; set; }

        private static string ReferenceId
        {
            get { return HttpContext.Current.Request["id"]; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (string.IsNullOrEmpty(ReferenceId))
            {
                //HttpContext.Current.Response.Redirect(string.Format("~/{0}.aspx?step={1}", AspxPageName, BillingSteps.BillingStepSuccess));
                HttpContext.Current.Response.Redirect(this.BillingSessionManager.ReturnUrl);
                return;
            }

            var url = HttpContext.Current.Request.Url.AbsoluteUri + "&redirect=1";
            HttpContext.Current.Response.AddHeader("REFRESH", String.Format("10;URL={0}", url));

            var redirect = HttpContext.Current.Request.QueryString["redirect"];
            if (redirect == null)
            {
                return;
            }
            
            if (redirect != "1") return;

            HttpContext.Current.Response.ClearHeaders();
            if ((InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), this.Invoice.Status) == InvoiceStatus.Paid)
            {
                HttpContext.Current.Response.Redirect(string.Format("~/{0}?step={1}",AspxPageName, BillingSteps.BillingStepPaid));
                return;
            }
            {
                HttpContext.Current.Response.Redirect(string.Format("~/{0}?step={1}",AspxPageName, BillingSteps.BillingStepFail));
            }
        }

      
        

      


    }
}
