using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class AnalysisConfiguration : IEntityTypeConfiguration<Analysis>
{
    public void Configure(EntityTypeBuilder<Analysis> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Ignore(a => a.IpAddress);
        builder.Ignore(a => a.OsType);
        builder.Ignore(a => a.PhoneModel);
        builder.HasOne<Mentor>(a => a.Mentor).WithOne(a => a.Analysis).HasForeignKey<Mentor>(a => a.AnalysisId);

        builder.ToTable("Analyzes");
    }
}