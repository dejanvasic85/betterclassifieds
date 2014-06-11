using System.ComponentModel.DataAnnotations.Schema;
using Paramount.ApplicationBlock.Data;
using System.Data.Entity;
using Paramount.Betterclassifieds.Business.Models;

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

        public IDbSet<TutorAdModel> TutorAds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TutorAdModel>().ToTable("TutorAd");
            modelBuilder.Entity<TutorAdModel>().HasKey(k => k.OnlineAdId);
        }
    }
}
