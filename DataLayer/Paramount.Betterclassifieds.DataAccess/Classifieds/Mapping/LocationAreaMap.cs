using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class LocationAreaMap : EntityTypeConfiguration<LocationArea>
    {
        public LocationAreaMap()
        {
            // Primary Key
            this.HasKey(t => t.LocationAreaId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("LocationArea");
            this.Property(t => t.LocationAreaId).HasColumnName("LocationAreaId");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Location)
                .WithMany(t => t.LocationAreas)
                .HasForeignKey(d => d.LocationId);

        }
    }
}
