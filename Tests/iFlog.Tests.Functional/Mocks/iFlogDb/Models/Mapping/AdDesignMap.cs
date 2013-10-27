using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class AdDesignMap : EntityTypeConfiguration<AdDesign>
    {
        public AdDesignMap()
        {
            // Primary Key
            this.HasKey(t => t.AdDesignId);

            // Properties
            // Table & Column Mappings
            this.ToTable("AdDesign");
            this.Property(t => t.AdDesignId).HasColumnName("AdDesignId");
            this.Property(t => t.AdId).HasColumnName("AdId");
            this.Property(t => t.AdTypeId).HasColumnName("AdTypeId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.FirstAdDesignId).HasColumnName("FirstAdDesignId");

            // Relationships
            this.HasOptional(t => t.Ad)
                .WithMany(t => t.AdDesigns)
                .HasForeignKey(d => d.AdId);
            this.HasOptional(t => t.AdType)
                .WithMany(t => t.AdDesigns)
                .HasForeignKey(d => d.AdTypeId);

        }
    }
}
