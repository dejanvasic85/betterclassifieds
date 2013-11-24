using System;

namespace Paramount.Betterclassifieds.Domain.Membership
{
    public class UserProfile
    {
        public Guid? UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string SecondaryPhone { get; set; }
        public string PreferedContact { get; set; }
        public string BusinessName { get; set; }
        public string ABN { get; set; }
        public int Industry { get; set; }
        public int BusinessCategory { get; set; }
        public int ProfileVersion { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool NewsletterSubscription { get; set; }
    }
}
