﻿using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class ContactUsView
    {
        [Required]
        [Display(Name =  "Name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [EmailAddress]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(500)]
        [Required]
        public string Comment { get; set; }

        public bool GoogleCaptchaEnabled { get; set; }
        public string GoogleCaptchaKey { get; set; }    
    }
}