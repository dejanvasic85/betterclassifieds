using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventBookingTicketFieldConfiguration : EntityTypeConfiguration<EventBookingTicketField>
    {
        public EventBookingTicketFieldConfiguration()
        {
            ToTable("EventBookingTicketField");
            HasKey(prop => prop.EventBookingTicketFieldId);
        }
    }
}