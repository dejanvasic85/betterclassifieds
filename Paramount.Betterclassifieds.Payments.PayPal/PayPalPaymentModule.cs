using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Payments.pp
{
    public class PayPalPaymentModule : ModuleRegistration
    {
        public const string PayPalServiceName = "PayPalService";

        public override string Name
        {
            get { return PayPalServiceName; }
        }

        public override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IPaymentService, PayPalPaymentService>(PayPalServiceName);
        }
    }
}
