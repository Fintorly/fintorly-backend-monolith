using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class PortfolioConfiguration:IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.IpAddress).IsRequired(false);
        builder.Property(a => a.OsType).IsRequired(false);
        builder.Property(a => a.PhoneModel).IsRequired(false);
        
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Portfolios).HasForeignKey(a=>a.MentorId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<User>(a => a.User).WithMany(a => a.Portfolios).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);


        builder.ToTable("Portfolios");
    }
}