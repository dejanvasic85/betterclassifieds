using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class EmailTemplateConfiguration : EntityTypeConfiguration<EmailTemplate>
    {
        public EmailTemplateConfiguration()
        {
            ToTable("EmailTemplate");

            HasKey(k => k.EmailTemplateId);

            Property(prop => prop.DocType).HasMaxLength(50);
            Property(prop => prop.Description).HasMaxLength(200);
            Property(prop => prop.SubjectTemplate).HasMaxLength(100);
            Property(prop => prop.From).HasMaxLength(200);
            Property(prop => prop.ParserName).HasMaxLength(50);
            Property(prop => prop.ModifiedBy).HasMaxLength(50);
        }
    }
}