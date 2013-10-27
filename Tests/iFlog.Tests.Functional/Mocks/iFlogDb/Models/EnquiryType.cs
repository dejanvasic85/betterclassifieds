using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class EnquiryType
    {
        public EnquiryType()
        {
            this.OnlineAdEnquiries = new List<OnlineAdEnquiry>();
        }

        public int EnquiryTypeId { get; set; }
        public string Title { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public virtual ICollection<OnlineAdEnquiry> OnlineAdEnquiries { get; set; }
    }
}
