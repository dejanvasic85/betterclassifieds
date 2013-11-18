using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class AdTypeMap : EntityTypeConfiguration<AdType>
    {
        public AdTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AdTypeId);

            // Properties
            this.Property(t => t.AdTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("AdType");
            this.Property(t => t.AdTypeId).HasColumnName("AdTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PaperBased).HasColumnName("PaperBased");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
