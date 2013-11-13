using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class TutorAdMap : EntityTypeConfiguration<TutorAd>
    {
        public TutorAdMap()
        {
            // Primary Key
            this.HasKey(t => t.TutorAdId);

            // Properties
            this.Property(t => t.Subjects)
                .HasMaxLength(500);

            this.Property(t => t.ExpertiseLevel)
                .HasMaxLength(100);

            this.Property(t => t.TravelOption)
                .HasMaxLength(50);

            this.Property(t => t.PricingOption)
                .HasMaxLength(100);

            this.Property(t => t.WhatToBring)
                .HasMaxLength(100);

            this.Property(t => t.Objective)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("TutorAd");
            this.Property(t => t.TutorAdId).HasColumnName("TutorAdId");
            this.Property(t => t.OnlineAdId).HasColumnName("OnlineAdId");
            this.Property(t => t.Subjects).HasColumnName("Subjects");
            this.Property(t => t.AgeGroupMin).HasColumnName("AgeGroupMin");
            this.Property(t => t.AgeGroupMax).HasColumnName("AgeGroupMax");
            this.Property(t => t.ExpertiseLevel).HasColumnName("ExpertiseLevel");
            this.Property(t => t.TravelOption).HasColumnName("TravelOption");
            this.Property(t => t.PricingOption).HasColumnName("PricingOption");
            this.Property(t => t.WhatToBring).HasColumnName("WhatToBring");
            this.Property(t => t.Objective).HasColumnName("Objective");

            // Relationships
            this.HasRequired(t => t.OnlineAd)
                .WithMany(t => t.TutorAds)
                .HasForeignKey(d => d.OnlineAdId);

        }
    }
}
