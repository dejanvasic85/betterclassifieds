using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class OnlineAdEnquiryMap : EntityTypeConfiguration<OnlineAdEnquiry>
    {
        public OnlineAdEnquiryMap()
        {
            // Primary Key
            this.HasKey(t => t.OnlineAdEnquiryId);

            // Properties
            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("OnlineAdEnquiry");
            this.Property(t => t.OnlineAdEnquiryId).HasColumnName("OnlineAdEnquiryId");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.OnlineAdId).HasColumnName("OnlineAdId");
            this.Property(t => t.EnquiryTypeId).HasColumnName("EnquiryTypeId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.EnquiryText).HasColumnName("EnquiryText");
            this.Property(t => t.OpenDate).HasColumnName("OpenDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Active).HasColumnName("Active");

            // Relationships
            this.HasRequired(t => t.EnquiryType)
                .WithMany(t => t.OnlineAdEnquiries)
                .HasForeignKey(d => d.EnquiryTypeId);
            this.HasRequired(t => t.OnlineAd)
                .WithMany(t => t.OnlineAdEnquiries)
                .HasForeignKey(d => d.OnlineAdId);

        }
    }
}
