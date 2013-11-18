using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class WebAdSpaceMap : EntityTypeConfiguration<WebAdSpace>
    {
        public WebAdSpaceMap()
        {
            // Primary Key
            this.HasKey(t => t.WebAdSpaceId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.AdLinkUrl)
                .HasMaxLength(255);

            this.Property(t => t.AdTarget)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            this.Property(t => t.DisplayText)
                .HasMaxLength(100);

            this.Property(t => t.ToolTipText)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("WebAdSpace");
            this.Property(t => t.WebAdSpaceId).HasColumnName("WebAdSpaceId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.SettingID).HasColumnName("SettingID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.AdLinkUrl).HasColumnName("AdLinkUrl");
            this.Property(t => t.AdTarget).HasColumnName("AdTarget");
            this.Property(t => t.SpaceType).HasColumnName("SpaceType");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.DisplayText).HasColumnName("DisplayText");
            this.Property(t => t.ToolTipText).HasColumnName("ToolTipText");
            this.Property(t => t.Active).HasColumnName("Active");

            // Relationships
            this.HasOptional(t => t.WebAdSpaceSetting)
                .WithMany(t => t.WebAdSpaces)
                .HasForeignKey(d => d.SettingID);

        }
    }
}
