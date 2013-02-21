using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Billing.UI.Enums;

namespace Paramount.Billing.UI.Invoice
{
    public abstract class BillingWizardStep : BillingCompositeUpdateControl
    {
        public string AspxPageName { get; set; }
         
        public void GoNext()
        {
            this.Page.Response.Redirect(string.Format("~/{0}?step=next", AspxPageName));
        }

        public void GoTo(BillingSteps step)
        {
            this.Page.Response.Redirect(string.Format("~/{0}?step={1}", AspxPageName,step));
        }

        public void GoPrevious()
        {
            this.Page.Response.Redirect(string.Format("~/{0}?step=prev", AspxPageName));
        }
    }
}
