using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class UserAndTokenConfiguration:IEntityTypeConfiguration<UserAndToken>
{
    public void Configure(EntityTypeBuilder<UserAndToken> builder)
    {
        builder.HasKey(a => new { a.UserId, a.TokenId });

        builder.HasOne<User>(a => a.User).WithMany(a => a.InterestedTokens).HasForeignKey(a => a.UserId);
        builder.HasOne<Token>(a => a.Token).WithMany(a => a.InterestedTokens).HasForeignKey(a => a.TokenId);
        builder.ToTable("UserAndTokens");
    }
}