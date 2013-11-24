using System.Data.Entity;

namespace Paramount.Betterclassifieds.DataAccess.Membership
{
    public class NotificationDbContext : DbContext
    {
        static NotificationDbContext()
        {
            Database.SetInitializer<NotificationDbContext>(null);
        }

        public NotificationDbContext()
            : base("NotificationDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Domain.Notifications.Email> EmailEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mapping.EmailEntryMapping());
        }
    }
}