namespace Paramount.Betterclassifieds.DataService.Repository
{
    using System;
    using Business.Payment;
    using DataService.Classifieds;

    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public PaymentsRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void CreateTransaction(string userId, string reference, string description, decimal amount, PaymentType paymentType)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                context.Transactions.InsertOnSubmit(
                    new Transaction
                    {
                        Amount = amount,
                        Description = description,
                        Title = reference,
                        TransactionDate = DateTime.Now,
                        UserId = userId,
                        TransactionType = (int)paymentType
                    }
                    );

                context.SubmitChanges();
            }
        }
    }
}