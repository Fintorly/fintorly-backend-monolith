using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class AdvertisementLogConfiguration : IEntityTypeConfiguration<AdvertisementLog>
{
    public void Configure(EntityTypeBuilder<AdvertisementLog> builder)
    {
        builder.HasKey(a => new { a.AdvertisementId, a.MentorId });

        builder.Ignore(a => a.IpAddress);
        builder.Ignore(a => a.OsType);
        builder.Ignore(a => a.PhoneModel);

        builder.ToTable("AdvertisementLogs");
    }
}