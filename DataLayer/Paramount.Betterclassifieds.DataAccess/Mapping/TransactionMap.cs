using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionId);

            // Properties
            this.Property(t => t.UserId)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(255);

            this.Property(t => t.RowTimeStamp)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Transaction");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.RowTimeStamp).HasColumnName("RowTimeStamp");
        }
    }
}
