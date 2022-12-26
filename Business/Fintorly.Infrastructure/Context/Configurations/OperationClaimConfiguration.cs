
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class OperationClaimConfiguration:IEntityTypeConfiguration<OperationClaim>
{

    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Ignore(a => a.IpAddress);
        builder.Ignore(a => a.OsType);
        builder.Ignore(a => a.PhoneModel);
        
        builder.Property(a => a.Name).IsRequired();
        
        builder.ToTable("OperationClaims");

        builder.HasData(
            new OperationClaim()
            {
                Name = "User",
                CreatedDate = DateTime.Now
            },
            new OperationClaim()
            {
                Name = "Mentor",
                CreatedDate = DateTime.Now
            },
            new OperationClaim()
            {
                Name = "Admin",
                CreatedDate = DateTime.Now
            },
            new OperationClaim()
            {
                Name = "Guest",
                CreatedDate = DateTime.Now
            });
    }
}