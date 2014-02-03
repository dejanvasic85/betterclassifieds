using System;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class PaymentsRepository : IPaymentsRepository
    {
        public void CreateTransaction(string userId, string reference, string description, decimal amount, PaymentType paymentType)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
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