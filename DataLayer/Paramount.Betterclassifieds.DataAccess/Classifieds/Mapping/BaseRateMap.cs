using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class BaseRateMap : EntityTypeConfiguration<BaseRate>
    {
        public BaseRateMap()
        {
            // Primary Key
            this.HasKey(t => t.BaseRateId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BaseRate");
            this.Property(t => t.BaseRateId).HasColumnName("BaseRateId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.UpgradeBaseRateId).HasColumnName("UpgradeBaseRateId");
        }
    }
}
