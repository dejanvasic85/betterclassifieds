using BetterClassified.Repository;

namespace Paramount.Betterclassifieds.Business
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