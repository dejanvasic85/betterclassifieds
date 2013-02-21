namespace Paramount.Common.DataTransferObjects.MembershipService
{
    public class ProfileInfo
    {
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondaryEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string SecondaryPhone { get; set; }
        public string BusinessName { get; set; }
        public string Abn { get; set; }
        public int?  Industry { get; set; }
        public int? AccountCategory { get; set; }
        public bool NewsletterSubscription { get; set; }


        public void Commit()
        {
            
        }
    }
}