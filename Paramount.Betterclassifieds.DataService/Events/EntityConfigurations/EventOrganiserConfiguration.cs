using Paramount.Betterclassifieds.Business.Events;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventOrganiserConfiguration : EntityTypeConfiguration<EventOrganiser>
    {
        public EventOrganiserConfiguration()
        {
            ToTable("EventOrganiser");
            HasKey(prop => prop.EventOrganiserId);
            Property(prop => prop.UserId).HasMaxLength(100);
            Property(prop => prop.Email).HasMaxLength(100);
            Property(prop => prop.LastModifiedBy).IsRequired().HasMaxLength(100);
            Property(prop => prop.LastModifiedDate).IsRequired();
            Property(prop => prop.LastModifiedDateUtc).IsRequired();
        }
    }
}
