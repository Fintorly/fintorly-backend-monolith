using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations;

public class PortfolioChartHistoryConfiguration : IEntityTypeConfiguration<PortfolioChartHistory>
{
    public void Configure(EntityTypeBuilder<PortfolioChartHistory> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.HasOne<Portfolio>(a => a.Portfolio).WithMany(a => a.PortfolioChartHistories)
            .HasForeignKey(a => a.PortfolioId);

        builder.ToTable("PortfolioChartHistories");
    }
}