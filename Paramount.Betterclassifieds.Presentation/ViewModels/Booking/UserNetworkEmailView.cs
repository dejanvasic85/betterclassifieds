using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class UserNetworkEmailView
    {
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Send")]
        public bool IsSelected { get; set; }
    }
}