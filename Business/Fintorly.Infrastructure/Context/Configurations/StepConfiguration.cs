using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(EntityTypeBuilder<Step> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Segment).IsRequired();
        builder.HasOne<Mentor>(a => a.Mentor).WithOne(a => a.Step).HasForeignKey<Mentor>(a => a.StepId);

        builder.ToTable("Steps");
    }
}