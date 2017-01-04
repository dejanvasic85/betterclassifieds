using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class UpdateEventTicketViewModel : IValidatableObject
    {
        public EventTicketViewModel EventTicket { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (EventTicket == null)
            {
                results.Add(new ValidationResult("Ticket cannot be null", new[] { nameof(EventTicket) }));
            }

            return results;
        }
    }
}