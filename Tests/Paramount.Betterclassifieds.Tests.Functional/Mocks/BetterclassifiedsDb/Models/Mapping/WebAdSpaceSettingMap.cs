using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class WebAdSpaceSettingMap : EntityTypeConfiguration<WebAdSpaceSetting>
    {
        public WebAdSpaceSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.SettingId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("WebAdSpaceSetting");
            this.Property(t => t.SettingId).HasColumnName("SettingId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.LocationCode).HasColumnName("LocationCode");
        }
    }
}
