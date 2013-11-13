using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class SchemaVersionMap : EntityTypeConfiguration<SchemaVersion>
    {
        public SchemaVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ScriptName)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("SchemaVersions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ScriptName).HasColumnName("ScriptName");
            this.Property(t => t.Applied).HasColumnName("Applied");
        }
    }
}
