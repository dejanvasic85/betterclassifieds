using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class UserNetworkEmailView
    {
        [Display(Name = "Full Name")]
        [StringLength(100)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Send")]
        public bool Selected { get; set; }
    }
}