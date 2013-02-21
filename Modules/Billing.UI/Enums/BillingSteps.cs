using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Billing.UI.Enums
{
    public enum BillingSteps
    {
           BillingStepAddress,
        BillingStepPaymentOption,
        BillingStepPayment,
        BillingStepFinalInvoice,
        None,
        BillingStepSuccess,
        BillingStepPaid,
        BillingStepCcNotify,
        BillingStepPpNotify,
        BillingStepCancelPurchase,
        BillingStepFail
    }
}
