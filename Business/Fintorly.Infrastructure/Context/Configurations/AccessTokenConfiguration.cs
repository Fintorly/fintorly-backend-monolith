using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class AccessTokenConfiguration:IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Token).IsRequired();
        
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.AccessTokens).HasForeignKey(a => a.MentorId);
        builder.HasOne<User>(a => a.User).WithMany(a => a.AccessTokens).HasForeignKey(a => a.UserId);

        builder.ToTable("ValidateTokens");
    }
}