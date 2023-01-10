using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class ApplicationRequestConfiguration : IEntityTypeConfiguration<ApplicationRequest>
{
    public void Configure(EntityTypeBuilder<ApplicationRequest> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.RejectionReason).HasMaxLength(100);
        builder.Property(a => a.RejectionReason).IsRequired(false);
        builder.Property(a => a.IsAccepted).HasDefaultValue("false");

        builder.HasOne<Mentor>(a => a.Mentor).WithOne(a => a.ApplicationRequest)
            .HasForeignKey<Mentor>(a => a.ApplicationRequestId);
        builder.ToTable("ApplicationRequests");
    }
}