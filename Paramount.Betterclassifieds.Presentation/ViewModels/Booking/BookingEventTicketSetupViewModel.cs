using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Monads;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingEventTicketSetupViewModel : IValidatableObject
    {
        public List<BookingEventTicketViewModel> Tickets { get; set; }
        
        public decimal EventTicketFee { get; set; }
        public decimal EventTicketFeeCents { get; set; }
        public bool? IncludeTransactionFee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Tickets != null)
            {
                foreach (var ticket in Tickets)
                {
                    if (ticket.EventTicketFields == null)
                        continue;

                    var uniqueFieldCount = ticket.With(t => t.EventTicketFields).Select(t => t.FieldName).Distinct().Count();
                    if (uniqueFieldCount < ticket.With(t => t.EventTicketFields).Count)
                    {
                        results.Add(new ValidationResult($"Ticket '{ticket.TicketName}' must have unique field names", new[] { "TicketFields" }));
                    }
                }
            }

            return results;
        }
    }
}