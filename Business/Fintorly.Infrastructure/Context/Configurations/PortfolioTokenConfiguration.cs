using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class PortfolioTokenConfiguration : IEntityTypeConfiguration<PortfolioToken>
{
    public void Configure(EntityTypeBuilder<PortfolioToken> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Symbol).IsRequired();
        builder.Property(a => a.BaseAsset).IsRequired(false);
        builder.Property(a => a.BaseAssetName).IsRequired();
        builder.HasOne<Portfolio>(a => a.Portfolio).WithMany(a => a.PortfolioTokens).HasForeignKey(a => a.PortfolioId);
        builder.ToTable("PortfolioTokens");
    }
}