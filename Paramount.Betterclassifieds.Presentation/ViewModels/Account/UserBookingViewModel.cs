namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class UserBookingViewModel
    {
        public int AdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string AdImageId { get; set; }
        public string Starts { get; set; }
        public string Ends { get; set; }
        public int Visits { get; set; }
        public int MessageCount { get { return this.Messages.Length; } }
        public string Status { get; set; }
        public AdEnquiryViewModel[] Messages { get; set; }
    }

}