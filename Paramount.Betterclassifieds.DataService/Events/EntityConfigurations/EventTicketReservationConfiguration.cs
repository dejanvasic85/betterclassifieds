using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventTicketReservationConfiguration : EntityTypeConfiguration<EventTicketReservation>
    {
        public EventTicketReservationConfiguration()
        {
            ToTable("EventTicketReservation");
            HasKey(prop => prop.EventTicketReservationId);
            Property(prop => prop.StatusAsString).HasColumnName("Status");
            Ignore(prop => prop.Status);
        }
    }
}