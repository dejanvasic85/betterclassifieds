using System.Configuration;
using System.Data.Entity;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.DataService
{
    public class ClassifiedsEntityContext : DbContext
    {
        public ClassifiedsEntityContext()
            : base(ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<OnlineAdRate> OnlineAdRates { get; set; }
        public IDbSet<UserNetworkModel> UserNetworks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNetworkModel>().ToTable("UserNetwork");
            modelBuilder.Entity<UserNetworkModel>().HasKey(k => k.UserNetworkId);

            modelBuilder.Entity<OnlineAdRate>().ToTable("OnlineAdRate");
            modelBuilder.Entity<OnlineAdRate>().HasKey(k => k.OnlineAdRateId);
            modelBuilder.Entity<OnlineAdRate>().Property(prop => prop.CategoryId).HasColumnName("MainCategoryId");
        }
    }
}
