using Paramount.ApplicationBlock.Data;
using System.Data.Entity;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.DataService
{
    public class ClassifiedsEntityContext : DbContext
    {
        public ClassifiedsEntityContext()
            : base(ConfigReader.GetConnectionString("paramount/services", "BetterclassifiedsConnection"))
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<OnlineAdRate> OnlineAdRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OnlineAdRate>().ToTable("OnlineAdRate");
            modelBuilder.Entity<OnlineAdRate>().HasKey(k => k.OnlineAdRateId);
            modelBuilder.Entity<OnlineAdRate>().Property(prop => prop.CategoryId).HasColumnName("MainCategoryId");
        }
    }
}
