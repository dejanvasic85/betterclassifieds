namespace Paramount.Betterclassifieds.Business.Models
{
    public class AdEnquiry
    {
        public int AdId { get; set; }
        public int OnlineAdId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Question { get; set; }
    }
}