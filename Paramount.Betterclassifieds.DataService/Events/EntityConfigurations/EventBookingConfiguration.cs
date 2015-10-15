using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventBookingConfiguration : EntityTypeConfiguration<EventBooking>
    {
        public EventBookingConfiguration()
        {
            ToTable("EventBooking");
            HasKey(prop => prop.EventBookingId);
            HasRequired(prop => prop.Event).WithMany(prop=> prop.EventBookings).HasForeignKey(prop => prop.EventId);
            Property(prop => prop.StatusAsString).HasColumnName("Status");
            Ignore(prop => prop.Status);
        }
    }

    public class EventBookingTicketConfiguration : EntityTypeConfiguration<EventBookingTicket>
    {
        public EventBookingTicketConfiguration()
        {
            ToTable("EventBookingTicket");
            HasKey(prop => prop.EventBookingTicketId);
            HasRequired(prop=> prop.EventTicket).WithMany(prop => prop.EventBookingTickets).HasForeignKey(prop => prop.EventTicketId);
            HasRequired(prop => prop.EventBooking).WithMany(prop => prop.EventBookingTickets).HasForeignKey(prop => prop.EventBookingId);
        }
    }
}