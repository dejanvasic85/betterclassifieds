namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPaymentService
    {
        PaymentResponse SubmitPayment(PaymentRequest request);
        void CompletePayment(string payReference, string payerId);
        void CompletePayment(string payReference, string payerId, string userId, decimal amount, string title, string description);
    }
}