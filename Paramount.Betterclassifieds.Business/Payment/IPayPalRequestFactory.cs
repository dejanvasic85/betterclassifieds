using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPayPalRequestFactory<in TModel> where TModel : class
    {
        PaymentRequest CreatePaymentRequest(TModel model, string payReference, string returnUrl, string cancelUrl);
    }
}