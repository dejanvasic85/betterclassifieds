namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPaymentService
    {
        PaymentResponse SubmitPayment(IPaymentRequest request);
        void CompletePayment(IPaymentRequest paymentRequest);
    }
}