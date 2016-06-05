using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class AddEventGuestViewModel : IValidatableObject
    {
        [Required]
        public int? EventId { get; set; }
        [Required]
        public int? Id { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public bool SendEmailToGuest { get; set; }
        public EventTicketViewModel SelectedTicket { get; set; }
        public List<EventTicketFieldViewModel> TicketFields { get; set; }
        public List<EventTicketViewModel> EventTickets { get; set; }
        public string FirstName => GuestFullName?.Split(' ').First();

        public string LastName
        {
            get
            {
                if (GuestFullName.IsNullOrEmpty())
                    return string.Empty;

                var sections = GuestFullName.Split(' ');
                if (sections.Length == 1)
                    return string.Empty;
                return sections[1];
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (SelectedTicket?.EventTicketId == null)
            {
                results.Add(new ValidationResult("A ticket must be selected before booking it for a guest.", new[] { "Selected Ticket" }));
            }
            return results;
        }
    }
}