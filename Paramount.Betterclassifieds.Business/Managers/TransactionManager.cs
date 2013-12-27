using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public class TransactionManager
    {
        private readonly IPaymentsRepository paymentsRepository;

        public TransactionManager(IPaymentsRepository paymentsRepository)
        {
            this.paymentsRepository = paymentsRepository;
        }


    }
}