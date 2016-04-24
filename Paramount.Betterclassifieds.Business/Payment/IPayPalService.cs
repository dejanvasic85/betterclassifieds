namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPayPalService
    {
        PayPalResponse SubmitPayment(PayPalRequest request);
        void CompletePayment(string payReference, string payerId);
        void CompletePayment(string payReference, string payerId, string userId, decimal amount, string title, string description);
    }
}