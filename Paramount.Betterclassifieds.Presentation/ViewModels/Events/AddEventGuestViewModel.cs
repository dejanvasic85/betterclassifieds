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

        [MaxLength(100)]
        public string GuestFullName { get; set; }

        public bool IsPublic { get; set; }

        [RequiredIf("IsSeatedEvent", true, ValidationMessage = "Seat is required for a seated event")]
        public string SeatNumber { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string GuestEmail { get; set; }
        public bool SendEmailToGuest { get; set; }
        public EventTicketViewModel SelectedTicket { get; set; }
        public EventGroupViewModel SelectedGroup { get; set; }
        public List<EventTicketFieldViewModel> TicketFields { get; set; }
        public List<EventTicketViewModel> EventTickets { get; set; }
        public List<EventGroupViewModel> EventGroups { get; set; }


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

        public bool DisplayGuests { get; set; }
        public bool IsSeatedEvent { get; set; }

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