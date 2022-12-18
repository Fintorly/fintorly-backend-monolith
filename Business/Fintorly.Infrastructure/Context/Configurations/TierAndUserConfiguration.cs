using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class TierAndUserConfiguration:IEntityTypeConfiguration<TierAndUser>
{
    public void Configure(EntityTypeBuilder<TierAndUser> builder)
    {
        builder.HasKey(a => new { a.TierId, a.UserId });

        builder.HasOne<User>(a => a.User).WithMany(a => a.TierAndUsers).HasForeignKey(a=>a.UserId);
        builder.HasOne<Tier>(a => a.Tier).WithMany(a => a.TierAndUsers).HasForeignKey(a => a.TierId);
        
        builder.ToTable("TierAndUsers");
    }
}