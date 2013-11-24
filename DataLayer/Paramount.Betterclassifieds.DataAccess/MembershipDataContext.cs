namespace Paramount.Betterclassifieds.DataAccess.Membership
{
    using Domain.Membership;
    using System.Data.Entity;

    public class MembershipDbContext : DbContext
    {
        static MembershipDbContext()
        {
            Database.SetInitializer<MembershipDbContext>(null);
        }

        public MembershipDbContext()
            : base("MembershipDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>().HasKey(k => k.UserId).ToTable("aspnet_Memberships");
            modelBuilder.Entity<User>().HasKey(k => k.UserId).ToTable("aspnet_Users");
            modelBuilder.Entity<UserProfile>().HasKey(k => k.UserID).ToTable("UserProfile");
            modelBuilder.Entity<Application>().HasKey(k=>k.ApplicationId).ToTable("aspnet_Applications");
        }
    }
}
