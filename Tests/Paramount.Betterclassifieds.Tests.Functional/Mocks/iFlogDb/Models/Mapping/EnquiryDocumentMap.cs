using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class EnquiryDocumentMap : EntityTypeConfiguration<EnquiryDocument>
    {
        public EnquiryDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.EnquiryDocumentId);

            // Properties
            // Table & Column Mappings
            this.ToTable("EnquiryDocument");
            this.Property(t => t.EnquiryDocumentId).HasColumnName("EnquiryDocumentId");
            this.Property(t => t.OnlineAdEnquiryId).HasColumnName("OnlineAdEnquiryId");
            this.Property(t => t.DocumentId).HasColumnName("DocumentId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.OnlineAdEnquiry)
                .WithMany(t => t.EnquiryDocuments)
                .HasForeignKey(d => d.OnlineAdEnquiryId);

        }
    }
}
