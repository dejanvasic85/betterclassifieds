namespace Paramount.Betterclassifieds.Business
{
    public class AdEnquiry
    {
        public int? EnquiryId { get; set; }
        public int AdId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Question { get; set; }
    }
}