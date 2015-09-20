
namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class SuccessView
    {
        public bool IsBookingActive  { get; set; }
        public int AdId { get; set; }
        public string TitleSlug { get; set; }
        public UserNetworkEmailView[] ExistingUsers { get; set; }
        public string CategoryAdType { get; set; }
    }
}