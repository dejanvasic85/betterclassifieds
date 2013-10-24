using System;
using System.Data.Entity;

namespace iFlog.Tests.Functional.Mocks
{
    public class ClassifiedEfContext : DbContext, IDataManager
    {
        public IDataManager Initialise()
        {
            Database.SetInitializer(new CreateTestDataInitialiser());
            return this;
        }

        public int CreateAd(string adTitle)
        {
            // Todo - create new ad
            //MainCategory category = new MainCategory { Title = "Services" };
            //MainCategories.Add(category);
            //SaveChanges();
            //return category.MainCategoryId;
            throw new NotImplementedException();
        }

        public ClassifiedEfContext()
            : base("iFlog")
        { }

        public IDbSet<MainCategory> MainCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainCategory>()
                        .ToTable("MainCategory")
                        .HasKey(k => k.MainCategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}