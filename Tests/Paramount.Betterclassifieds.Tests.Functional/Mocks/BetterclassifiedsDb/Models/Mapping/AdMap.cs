using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class AdMap : EntityTypeConfiguration<Ad>
    {
        public AdMap()
        {
            // Primary Key
            this.HasKey(t => t.AdId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Comments)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Ad");
            this.Property(t => t.AdId).HasColumnName("AdId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.UseAsTemplate).HasColumnName("UseAsTemplate");
        }
    }
}
