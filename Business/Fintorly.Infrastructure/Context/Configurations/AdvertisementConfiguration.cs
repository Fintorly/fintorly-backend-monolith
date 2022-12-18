using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class AdvertisementConfiguration:IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title).IsRequired();
        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.Price).IsRequired();
        builder.Property(a => a.PackageType).IsRequired();

        builder.ToTable("Advertisements");
    }
}