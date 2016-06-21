using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventTicketConfiguration : EntityTypeConfiguration<EventTicket>
    {
        public EventTicketConfiguration()
        {
            ToTable("EventTicket");
            HasKey(prop => prop.EventTicketId);
            Property(prop => prop.Price).HasPrecision(19, 4);
        }
    }
}