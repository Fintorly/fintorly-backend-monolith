using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class UserAndOperationClaimConfigurations : IEntityTypeConfiguration<UserAndOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserAndOperationClaim> builder)
    {
        builder.HasKey(a => new {a.UserId,a.OperationClaimId});

        builder.HasOne<Domain.Entities.OperationClaim>(a => a.OperationClaim).WithMany(a => a.UserAndOperationClaims)
            .HasForeignKey(a => a.OperationClaimId);
        builder.HasOne<User>(a => a.User).WithMany(a => a.UserAndOperationClaims).HasForeignKey(a => a.UserId);

        builder.ToTable("UserAndOperationClaims");
    }
}