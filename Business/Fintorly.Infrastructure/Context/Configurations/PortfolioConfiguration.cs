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
        
        builder.HasOne<Mentor>(a => a.Mentor).WithMany(a => a.Portfolios).HasForeignKey(a=>a.MentorId);
        builder.HasOne<User>(a => a.User).WithMany(a => a.Portfolios).HasForeignKey(a => a.UserId);


        builder.ToTable("Portfolios");
    }
}