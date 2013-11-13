using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class EnquiryDocument
    {
        public int EnquiryDocumentId { get; set; }
        public int OnlineAdEnquiryId { get; set; }
        public Nullable<int> DocumentId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public virtual OnlineAdEnquiry OnlineAdEnquiry { get; set; }
    }
}
