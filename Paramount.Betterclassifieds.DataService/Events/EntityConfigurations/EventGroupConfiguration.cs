using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventGroupConfiguration : EntityTypeConfiguration<EventGroup>
    {
        public EventGroupConfiguration()
        {
            ToTable("EventGroup");

            HasKey(prop => prop.EventGroupId);
        }
    }
}