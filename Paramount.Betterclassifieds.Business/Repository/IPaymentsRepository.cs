namespace Paramount.Betterclassifieds.Business.Repository
{
    using Payment;

    public interface IPaymentsRepository
    {
        void CreateTransaction(string userId, string reference, string description, decimal amount, PaymentType paymentType);
    }
}