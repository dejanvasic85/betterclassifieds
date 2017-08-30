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

        public int EnquiryId { get; set; }
        public string FullName { get; set; }        
        public string Email { get; set; }
        public string EnquiryText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}