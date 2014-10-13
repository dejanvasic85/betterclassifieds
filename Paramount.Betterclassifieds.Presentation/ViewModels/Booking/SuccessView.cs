
namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class SuccessView
    {
        public bool IsBookingActive  { get; set; }
        public string AdId { get; set; }
        public UserNetworkEmailView[] ExistingUserNetwork { get; set; }
    }
}