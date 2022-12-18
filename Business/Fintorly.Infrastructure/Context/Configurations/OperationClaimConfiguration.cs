
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class OperationClaimConfiguration:IEntityTypeConfiguration<OperationClaim>
{

    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
        
        builder.ToTable("OperationClaims");
    }
}