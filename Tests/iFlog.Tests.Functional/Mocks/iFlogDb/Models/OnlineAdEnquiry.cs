using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class OnlineAdEnquiry
    {
        public OnlineAdEnquiry()
        {
            this.EnquiryDocuments = new List<EnquiryDocument>();
        }

        public int OnlineAdEnquiryId { get; set; }
        public string FullName { get; set; }
        public int OnlineAdId { get; set; }
        public int EnquiryTypeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EnquiryText { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> Active { get; set; }
        public virtual ICollection<EnquiryDocument> EnquiryDocuments { get; set; }
        public virtual EnquiryType EnquiryType { get; set; }
        public virtual OnlineAd OnlineAd { get; set; }
    }
}
