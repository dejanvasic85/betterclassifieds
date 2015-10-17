namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPaymentService
    {
        PaymentResponse SubmitPayment(PayPalPaymentRequest request);
        void CompletePayment(string payReference, string payerId);
    }
}