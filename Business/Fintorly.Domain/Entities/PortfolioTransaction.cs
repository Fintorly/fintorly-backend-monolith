using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class PortfolioTransaction : BaseEntity, IEntity
{
    public Guid PortfolioTokenId { get; set; }
    public PortfolioToken PortfolioToken { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public float PercentChange { get; set; }
    public decimal PriceChange { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
}