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
            HasRequired(prop => prop.EventTicket)
                .WithMany(prop => prop.EventTicketReservations)
                .HasForeignKey(prop => prop.EventTicketId);
            Property(prop => prop.StatusAsString).HasColumnName("Status");
            Property(prop => prop.Price).HasPrecision(19, 4);
            Property(prop => prop.TransactionFee).HasPrecision(19, 4);

            Ignore(prop => prop.Status);

            // We don't store the guest fields in the reservation
            Ignore(prop => prop.GuestFullName);
            Ignore(prop => prop.GuestEmail);
            Ignore(prop => prop.TicketFields);
        }
    }
}