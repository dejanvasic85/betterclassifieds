namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPaymentService
    {
        PaymentResponse SubmitPayment(PaymentRequest request);
        void CompletePayment(PaymentRequest paymentRequest);
    }
}