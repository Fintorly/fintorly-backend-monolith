using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MentorAndOperationClaimConfiguration:IEntityTypeConfiguration<MentorAndOperationClaim>
{
    public void Configure(EntityTypeBuilder<MentorAndOperationClaim> builder)
    {
        builder.HasKey(a => new { a.MentorId, a.OperationClaimId });

        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.MentorAndOperationClaims).HasForeignKey(a => a.MentorId);
        builder.HasOne<OperationClaim>(a => a.OperationClaim).WithMany(a => a.MentorAndOperationClaims)
            .HasForeignKey(a => a.OperationClaimId);

        builder.ToTable("MentorAndOperationClaims");
    }
}