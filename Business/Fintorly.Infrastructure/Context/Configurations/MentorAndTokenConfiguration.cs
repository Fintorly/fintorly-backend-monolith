using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class MentorAndTokenConfiguration : IEntityTypeConfiguration<MentorAndToken>
{
    public void Configure(EntityTypeBuilder<MentorAndToken> builder)
    {
        builder.HasKey(a => new { a.MentorId, a.TokenId });

        builder.HasOne(a => a.Token).WithMany(b => b.MentorAndTokens).HasForeignKey(a => a.TokenId);
        builder.HasOne(a => a.Mentor).WithMany(a => a.MentorAndTokens).HasForeignKey(a => a.MentorId);
        builder.ToTable("MentorAndTokens");
    }
}