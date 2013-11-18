using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class PublicationAdTypeMap : EntityTypeConfiguration<PublicationAdType>
    {
        public PublicationAdTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicationAdTypeId);

            // Properties
            // Table & Column Mappings
            this.ToTable("PublicationAdType");
            this.Property(t => t.PublicationAdTypeId).HasColumnName("PublicationAdTypeId");
            this.Property(t => t.PublicationId).HasColumnName("PublicationId");
            this.Property(t => t.AdTypeId).HasColumnName("AdTypeId");

            // Relationships
            this.HasOptional(t => t.AdType)
                .WithMany(t => t.PublicationAdTypes)
                .HasForeignKey(d => d.AdTypeId);
            this.HasOptional(t => t.Publication)
                .WithMany(t => t.PublicationAdTypes)
                .HasForeignKey(d => d.PublicationId);

        }
    }
}
