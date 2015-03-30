
namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class SuccessView
    {
        public bool IsBookingActive  { get; set; }
        public string AdId { get; set; }
        public UserNetworkEmailView[] ExistingUsers { get; set; }
    }
}