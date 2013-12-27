using System;
using BetterclassifiedsCore.DataModel;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class PaymentsRepository : IPaymentsRepository
    {
        public void CreateTransaction(string userId, string reference, string description, decimal amount, PaymentType paymentType)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
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