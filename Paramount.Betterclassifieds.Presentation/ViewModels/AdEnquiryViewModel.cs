using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdEnquiryViewModel
    {
        [Required]
        public int AdId { get; set; }

        [Display(Name = "Full Name")]
        [MaxLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "Your question cannot exceed 500 characters")]
        public string Question { get; set; }
    }
}