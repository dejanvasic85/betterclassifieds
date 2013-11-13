using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class MainCategoryMap : EntityTypeConfiguration<MainCategory>
    {
        public MainCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.MainCategoryId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            this.Property(t => t.OnlineAdTag)
                .HasMaxLength(50);

            this.HasOptional(x => x.ParentCategory)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .WillCascadeOnDelete(false);

            // Table & Column Mappings
            this.ToTable("MainCategory");
            this.Property(t => t.MainCategoryId).HasColumnName("MainCategoryId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.OnlineAdTag).HasColumnName("OnlineAdTag");
        }
    }
}
