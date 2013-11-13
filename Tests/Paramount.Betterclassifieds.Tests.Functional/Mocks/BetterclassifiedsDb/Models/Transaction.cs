using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public Nullable<int> TransactionType { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public byte[] RowTimeStamp { get; set; }
    }
}
