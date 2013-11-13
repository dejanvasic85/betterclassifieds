using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class AdGraphicMap : EntityTypeConfiguration<AdGraphic>
    {
        public AdGraphicMap()
        {
            // Primary Key
            this.HasKey(t => t.AdGraphicId);

            // Properties
            this.Property(t => t.DocumentID)
                .HasMaxLength(100);

            this.Property(t => t.Filename)
                .HasMaxLength(100);

            this.Property(t => t.ImageType)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AdGraphic");
            this.Property(t => t.AdGraphicId).HasColumnName("AdGraphicId");
            this.Property(t => t.AdDesignId).HasColumnName("AdDesignId");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.Filename).HasColumnName("Filename");
            this.Property(t => t.ImageType).HasColumnName("ImageType");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.AdDesign)
                .WithMany(t => t.AdGraphics)
                .HasForeignKey(d => d.AdDesignId);

        }
    }
}
