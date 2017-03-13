using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class AcceptOrganiserInviteViewModel
    {
        public bool IsSuccessful { get; set; }
        public string EventUrl { get; set; }
        public string EventName { get; set; }
        public bool BadRequest { get; set; }
        public bool AlreadyActivated { get; set; }
        public bool WrongEmail { get; set; }
        public string BrandName { get; set; }
    }

    public class AcceptOrganiserInviteRequestVm
    {
        [Required]
        public int? EventId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Recipient { get; set; }
    }
}