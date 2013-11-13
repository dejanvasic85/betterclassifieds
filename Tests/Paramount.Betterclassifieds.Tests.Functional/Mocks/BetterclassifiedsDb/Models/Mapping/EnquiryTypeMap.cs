using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class EnquiryTypeMap : EntityTypeConfiguration<EnquiryType>
    {
        public EnquiryTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EnquiryTypeId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EnquiryType");
            this.Property(t => t.EnquiryTypeId).HasColumnName("EnquiryTypeId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
