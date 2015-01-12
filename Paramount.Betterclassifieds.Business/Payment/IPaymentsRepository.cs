namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPaymentsRepository
    {
        void CreateTransaction(string userId, string reference, string description, decimal amount, PaymentType paymentType);
    }
}