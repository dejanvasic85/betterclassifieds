namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IPaymentsRepository
    {
        void CreateTransaction(string userId, string reference, string description, decimal amount, Models.PaymentType paymentType);
    }
}