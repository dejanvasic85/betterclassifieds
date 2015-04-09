using System;

namespace Paramount.Betterclassifieds.Business
{
    public class Enquiry
    {
        public Enquiry()
        { }

        public Enquiry(int enquiryId, string fullName, string email, string enquiryText, DateTime createdDate, bool active)
        {
            EnquiryId = enquiryId;
            FullName = fullName;
            Email = email;
            EnquiryText = enquiryText;
            CreatedDate = createdDate;
            Active = active;
        }

        public int EnquiryId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string EnquiryText { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public bool Active { get; private set; }
    }
}