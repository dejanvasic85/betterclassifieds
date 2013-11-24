using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class PublicationCategoryMap : EntityTypeConfiguration<PublicationCategory>
    {
        public PublicationCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicationCategoryId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("PublicationCategory");
            this.Property(t => t.PublicationCategoryId).HasColumnName("PublicationCategoryId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.MainCategoryId).HasColumnName("MainCategoryId");
            this.Property(t => t.PublicationId).HasColumnName("PublicationId");

            // Relationships
            this.HasOptional(t => t.MainCategory)
                .WithMany(t => t.PublicationCategories)
                .HasForeignKey(d => d.MainCategoryId);
            this.HasOptional(t => t.Publication)
                .WithMany(t => t.PublicationCategories)
                .HasForeignKey(d => d.PublicationId);

        }
    }
}
