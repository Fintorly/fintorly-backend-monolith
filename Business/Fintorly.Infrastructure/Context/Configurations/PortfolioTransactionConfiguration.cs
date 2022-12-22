using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class PortfolioTransactionConfiguration : IEntityTypeConfiguration<PortfolioTransaction>
{
    public void Configure(EntityTypeBuilder<PortfolioTransaction> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne<PortfolioToken>(a => a.PortfolioToken).WithMany(a => a.PortfolioTransactions)
            .HasForeignKey(a => a.PortfolioTokenId);

        builder.ToTable("PortfolioTransactions");
    }
}