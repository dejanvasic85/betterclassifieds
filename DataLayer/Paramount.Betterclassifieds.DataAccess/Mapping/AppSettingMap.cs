using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class AppSettingMap : EntityTypeConfiguration<AppSetting>
    {
        public AppSettingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Module, t.AppKey });

            // Properties
            this.Property(t => t.Module)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.AppKey)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DataType)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AppSetting");
            this.Property(t => t.Module).HasColumnName("Module");
            this.Property(t => t.AppKey).HasColumnName("AppKey");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.SettingValue).HasColumnName("SettingValue");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
