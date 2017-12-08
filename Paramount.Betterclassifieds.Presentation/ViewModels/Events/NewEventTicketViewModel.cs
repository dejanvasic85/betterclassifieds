using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class NewEventTicketViewModel : IValidatableObject
    {
        [Required]
        public int? EventId { get; set; }

        [Required]
        public string TicketName { get; set; }

        public decimal? Price { get; set; }

        public int AvailableQuantity { get; set; }

        public string ColourCode { get; set; }

        public string TicketImage { get; set; }

        public EventTicketFieldViewModel[] EventTicketFields { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (EventTicketFields == null)
                return results;

            var uniqueFieldCount = EventTicketFields.Select(t => t.FieldName).Distinct().Count();
            if (uniqueFieldCount < EventTicketFields.Length)
            {
                results.Add(new ValidationResult($"Ticket '{TicketName}' must have unique field names", new[] { "Fields" }));
            }

            return results;
        }
    }
}