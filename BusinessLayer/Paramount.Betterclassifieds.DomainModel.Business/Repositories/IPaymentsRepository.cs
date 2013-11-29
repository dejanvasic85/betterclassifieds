
using Paramount.DomainModel.Business.Betterclassifieds.Enums;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface IPaymentsRepository
    {
        void CreateTransaction(string userId, string reference, string description, decimal amount, PaymentType paymentType);
    }
}