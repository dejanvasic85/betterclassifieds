using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class SupportEnquiry
    {
        public int SupportEnquiryId { get; set; }
        public string EnquiryTypeName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string EnquiryText { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
