using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class PortfolioChartHistory : BaseEntity, IEntity
{
    public Guid PortfolioId { get; set; }
    public Portfolio Portfolio { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceChange { get; set; }
    public decimal TotalPriceChangePercent { get; set; }
    public decimal TotalPriceUser24Hour { get; set; }
}