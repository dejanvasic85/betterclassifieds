using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.Billing.UI.Enums;
using Paramount.Common.DataTransferObjects.Enums;

namespace Paramount.Billing.UI
{
    public class SessionManager
    {
        public HttpContext Context { get; set; }

        public const string BillingStepAddress = "BillingStepAddress";
        public const string BillingStepPaymentOption = "BillingStepPaymentOption";
        public const string BillingStepPayment = "BillingStepPayment";
        public const string BillingStepFinalInvoice = "BillingStepFinalInvoice";

        //private static SessionManager sessionManager;

        protected SessionManager(HttpContext context)
        {
            this.Context = context;
        }

        protected SessionManager()
        {
            this.Context = HttpContext.Current;
        }

        protected void Add(string key, object item)
        {
            Context.Session[key] = item;
        }

        protected void Clear(string key)
        {
            Context.Session.Remove(key);
        }

        protected object Get(string key)
        {
            return Context.Session[key];
        }

        protected Guid Get(string key, Guid defaultValue)
        {
            var value = Context.Session[key];
            return value == null ? defaultValue : (Guid)value;
        }

        protected void Remove(string key)
        {
            if (Context.Session[key] != null)
            {
                Context.Session.Remove(key);
            }
        }
    }

    public class BillingSessionManager : SessionManager
    {
        const string InvoiceIdKey = "InvoiceId";
        const string CartIdKey = "CartId";
        const string BillingCurrentStepKey = "BillingCurrentStep";
        const string ReturnUrlKey = "ReturnUrl";
        const string CancelUrlKey = "CancelUrl";
        const string PaymentTypeKey = "PaymentType";
        const string ProductTypeKey = "ProductType";

        public static BillingSessionManager Instance
        {
            get
            {
               // return sessionManager ?? (sessionManager = new BillingSessionManager());
                return new BillingSessionManager();
            }
        }

        public static BillingSessionManager GetInstance(HttpContext context)
        {
            return new BillingSessionManager(){Context = context};
        }

        public Guid InvoiceId
        {
            get
            {
                
                return this.Get(InvoiceIdKey, Guid.Empty);
            }
            set
            {
                this.Add(InvoiceIdKey, value);
            }
        }

        public Guid CartId
        {
            get
            {
                
                return this.Get(CartIdKey, Guid.Empty);
            }
            set
            {
                this.Add(CartIdKey, value);
            }
        }
        
        public BillingSteps BillingCurrentStep
        {
            get
            {
                
                if (Get(BillingCurrentStepKey) == null)
                {
                    return BillingSteps.None;
                }
                return (BillingSteps)Enum.Parse(typeof(BillingSteps), Get(BillingCurrentStepKey).ToString());
            }
            set
            {
                this.Add(BillingCurrentStepKey, value);
            }
        }

        public string ReturnUrl
        {
            get
            {
                
                return this.Get(ReturnUrlKey).ToString();
            }
            set
            {
                this.Add(ReturnUrlKey, value);
            }
        }

        public string CancelUrl
        {
            get
            {
                
                return this.Get(CancelUrlKey).ToString();
            }
            set
            {
                this.Add(CancelUrlKey, value);
            }
        }

        public PaymentType PaymentType
        {
            get
            {
                
                if (Get(PaymentTypeKey) == null)
                {
                    return PaymentType.None;
                }
                return (PaymentType)Enum.Parse(typeof(PaymentType), Get(PaymentTypeKey).ToString());
            }
            set
            {
                this.Add(PaymentTypeKey, value);
            }
        }

        public string ProductType
        {
            get
            {
                
                return this.Get(ProductTypeKey).ToString();
            }
            set
            {
                this.Add(ProductTypeKey, value);
            }
        }

        public  void Clear()
        {
            Clear(InvoiceIdKey);
            Clear(CartIdKey);
            Clear(BillingCurrentStepKey);
            Clear(ReturnUrlKey);
            Clear(CancelUrlKey);
            Clear(PaymentTypeKey);
            Clear(ProductTypeKey);
        }
    }
}
