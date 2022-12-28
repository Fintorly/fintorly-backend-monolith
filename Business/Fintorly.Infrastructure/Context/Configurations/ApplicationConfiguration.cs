using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class ApplicationConfiguration:IEntityTypeConfiguration<ApplicationRequest>
{
    public void Configure(EntityTypeBuilder<ApplicationRequest> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.RejectionReason).IsRequired(false);
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        builder.Property(a => a.AdminId).IsRequired(false);
        
        builder.HasOne<Mentor>(a => a.Mentor).WithOne(a => a.ApplicationRequest)
            .HasForeignKey<ApplicationRequest>(a => a.MentorId);

        builder.ToTable("ApplicationRequests");
    }
}