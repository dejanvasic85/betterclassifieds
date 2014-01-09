using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public class TransactionManager
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public TransactionManager(IPaymentsRepository paymentsRepository)
        {
            this._paymentsRepository = paymentsRepository;
        }


    }
}