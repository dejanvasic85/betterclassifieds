using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class LookupMap : EntityTypeConfiguration<Lookup>
    {
        public LookupMap()
        {
            // Primary Key
            this.HasKey(t => t.LookupId);

            // Properties
            this.Property(t => t.GroupName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LookupValue)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Lookup");
            this.Property(t => t.LookupId).HasColumnName("LookupId");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.LookupValue).HasColumnName("LookupValue");
        }
    }
}
