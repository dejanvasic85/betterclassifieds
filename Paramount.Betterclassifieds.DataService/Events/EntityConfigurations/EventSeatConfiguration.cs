using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventSeatConfiguration : EntityTypeConfiguration<EventSeat>
    {
        public EventSeatConfiguration()
        {
            ToTable("EventSeat");

            HasKey(prop => prop.EventSeatId);

            HasRequired(prop => prop.EventTicket).WithMany(t => t.EventSeats).HasForeignKey(k => k.EventTicketId);

            // Calculated field when being returned from stored procedure so we do not store this
            Ignore(prop => prop.IsBooked);
        }
    }
}