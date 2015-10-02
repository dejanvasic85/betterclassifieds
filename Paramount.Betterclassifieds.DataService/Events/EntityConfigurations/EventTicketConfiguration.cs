using System.ComponentModel.DataAnnotations.Schema;
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
        }
    }
}