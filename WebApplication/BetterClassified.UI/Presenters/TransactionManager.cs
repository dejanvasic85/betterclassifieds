namespace BetterClassified.UI.Presenters
{
    public class TransactionManager
    {
        private readonly Repository.IPaymentsRepository paymentsRepository;

        public TransactionManager(Repository.IPaymentsRepository paymentsRepository)
        {
            this.paymentsRepository = paymentsRepository;
        }
        

    }
}