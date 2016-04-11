using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventBookingTicketValidationConfiguration : EntityTypeConfiguration<EventBookingTicketValidation>
    {
        public EventBookingTicketValidationConfiguration()
        {
            ToTable("EventBookingTicketValidation");
            HasKey(p => p.EventBookingTicketValidationId);
        }
    }
}