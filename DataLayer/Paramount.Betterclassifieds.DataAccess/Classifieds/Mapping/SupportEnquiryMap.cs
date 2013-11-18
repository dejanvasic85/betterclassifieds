using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class SupportEnquiryMap : EntityTypeConfiguration<SupportEnquiry>
    {
        public SupportEnquiryMap()
        {
            // Primary Key
            this.HasKey(t => t.SupportEnquiryId);

            // Properties
            this.Property(t => t.EnquiryTypeName)
                .HasMaxLength(50);

            this.Property(t => t.FullName)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(15);

            this.Property(t => t.Subject)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SupportEnquiry");
            this.Property(t => t.SupportEnquiryId).HasColumnName("SupportEnquiryId");
            this.Property(t => t.EnquiryTypeName).HasColumnName("EnquiryTypeName");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.EnquiryText).HasColumnName("EnquiryText");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
