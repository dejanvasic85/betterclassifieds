using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventTicketFieldConfiguration : EntityTypeConfiguration<EventTicketField>
    {
        public EventTicketFieldConfiguration()
        {
            ToTable("EventTicketField");
            HasKey(prop => prop.EventTicketFieldId);
        }
    }
}