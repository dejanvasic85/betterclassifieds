using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class PublicationTypeMap : EntityTypeConfiguration<PublicationType>
    {
        public PublicationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicationTypeId);

            // Properties
            this.Property(t => t.PublicationTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("PublicationType");
            this.Property(t => t.PublicationTypeId).HasColumnName("PublicationTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
