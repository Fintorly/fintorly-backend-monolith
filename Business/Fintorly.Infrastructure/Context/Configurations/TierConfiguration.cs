using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class TierConfiguration:IEntityTypeConfiguration<Tier>
{
    public void Configure(EntityTypeBuilder<Tier> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.Title).IsRequired();
        builder.Property(a => a.Price).IsRequired();
        builder.Property(a => a.PackageType).IsRequired();

        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Tiers).HasForeignKey(a => a.MentorId);
        
        builder.ToTable("Tiers");
    }
}