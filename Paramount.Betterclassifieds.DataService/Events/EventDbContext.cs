using System.Configuration;
using System.Data.Entity;
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
        { }

        public EventDbContext()
            : this(ConfigurationManager.ConnectionStrings["EventsConnection"].ConnectionString)
        { }

        public IDbSet<EventModel> Events { get; set; }
        public IDbSet<EventTicket> EventTickets { get; set; }
        public IDbSet<EventTicketReservation> EventTicketReservations { get; set; }
        //public IDbSet<EventTicketBooking> EventTicketBookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EventModelConfiguration());
            modelBuilder.Configurations.Add(new EventTicketConfiguration());
            modelBuilder.Configurations.Add(new EventTicketReservationConfiguration());
        }
    }
}