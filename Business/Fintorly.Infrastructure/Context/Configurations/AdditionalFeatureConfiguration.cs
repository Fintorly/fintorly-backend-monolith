using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class AdditionalFeatureConfiguration:IEntityTypeConfiguration<AdditionalFeature>
{
    public void Configure(EntityTypeBuilder<AdditionalFeature> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Key).IsRequired();
        builder.Property(a => a.Value).IsRequired();
        
        builder.HasOne<Post>(a => a.Post).WithMany(a => a.AdditionalFeatures).HasForeignKey(a => a.PostId);
        builder.HasOne<Tier>(a => a.Tier).WithMany(a => a.AdditionalFeatures).HasForeignKey(a => a.TierId);
        builder.ToTable("AdditionalFeatures");
    }
}