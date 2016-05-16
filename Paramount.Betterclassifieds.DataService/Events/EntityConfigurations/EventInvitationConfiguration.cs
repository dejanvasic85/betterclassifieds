using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventInvitationConfiguration : EntityTypeConfiguration<EventInvitation>
    {
        public EventInvitationConfiguration()
        {
            ToTable("EventInvitation");
            HasKey(p => p.EventInvitationId);

            HasRequired(prop => prop.EventModel);
        }
    }
}