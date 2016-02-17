using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventModelConfiguration : EntityTypeConfiguration<EventModel>
    {
        public EventModelConfiguration()
        {
            ToTable("Event");   
            HasKey(p => p.EventId);
            HasOptional(p => p.Address);
        }
    }
}