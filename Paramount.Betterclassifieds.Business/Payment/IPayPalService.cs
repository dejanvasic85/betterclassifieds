namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPayPalService
    {
        PayPalResponse SubmitPayment(PayPalRequest request);
        bool CompletePayment(string payReference, string payerId);
        bool CompletePayment(string payReference, string payerId, string userId, decimal amount, string title, string description);
    }
}  