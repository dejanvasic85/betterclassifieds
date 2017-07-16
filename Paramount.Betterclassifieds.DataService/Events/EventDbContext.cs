using System.Configuration;
using System.Data.Entity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventDbContext : DbContext
    {
        static EventDbContext()
        {
            // Entity framework is crazy.
            // If we don't set a null ininitializer, the default one will create the database automatically
            Database.SetInitializer<EventDbContext>(null);
        }

        public EventDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public EventDbContext()
            : this(ConfigurationManager.ConnectionStrings["EventsConnection"].ConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<EventModel> Events { get; set; }
        public IDbSet<EventTicket> EventTickets { get; set; }
        public IDbSet<EventTicketField> EventTicketFields { get; set; }
        public IDbSet<EventTicketReservation> EventTicketReservations { get; set; }
        public IDbSet<EventBooking> EventBookings { get; set; }
        public IDbSet<EventBookingTicket> EventBookingTickets { get; set; }
        public IDbSet<EventBookingTicketField> EventBookingTicketFields { get; set; }
        public IDbSet<EventPaymentRequest> EventPaymentRequests { get; set; }
        public IDbSet<EventBookingTicketValidation> EventBookingTicketValidations { get; set; }
        public IDbSet<EventInvitation> EventInvitations { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<EventGroup> EventGroups { get; set; }
        public IDbSet<EventOrganiser> EventOrganisers { get; set; }
        public IDbSet<EventSeat> EventSeats { get; set; }
        public IDbSet<EventPromoCode> PromoCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EventModelConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new EventTicketConfiguration());
            modelBuilder.Configurations.Add(new EventTicketFieldConfiguration());
            modelBuilder.Configurations.Add(new EventTicketReservationConfiguration());
            modelBuilder.Configurations.Add(new EventBookingConfiguration());
            modelBuilder.Configurations.Add(new EventBookingTicketConfiguration());
            modelBuilder.Configurations.Add(new EventBookingTicketFieldConfiguration());
            modelBuilder.Configurations.Add(new EventPaymentRequestConfiguration());
            modelBuilder.Configurations.Add(new EventBookingTicketValidationConfiguration());
            modelBuilder.Configurations.Add(new EventInvitationConfiguration());
            modelBuilder.Configurations.Add(new EventGroupConfiguration());
            modelBuilder.Configurations.Add(new EventOrganiserConfiguration());
            modelBuilder.Configurations.Add(new EventSeatBookingConfiguration());
            modelBuilder.Configurations.Add(new EventPromoCodeConfiguration());
        }
    }
}