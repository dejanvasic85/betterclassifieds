using Paramount.Betterclassifieds.Business.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.DataService.Events.EntityConfigurations
{
    public class EventOrganiserConfiguration : EntityTypeConfiguration<EventOrganiser>
    {
        public EventOrganiserConfiguration()
        {
            ToTable("EventOrganiser");
            HasKey(prop => prop.EventOrganiserId);
            Property(prop => prop.UserId).IsRequired().HasMaxLength(100);
            Property(prop => prop.LastModifiedBy).IsRequired().HasMaxLength(100);
            Property(prop => prop.LastModifiedDate).IsRequired();
            Property(prop => prop.LastModifiedDateUtc).IsRequired();
        }
    }
}
