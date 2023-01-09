using Fintorly.Application.Dtos.PortfolioTokenDtos;

namespace Fintorly.Application.Dtos.PortfolioTransactionDtos;

public class PortfolioTransactionDto
{
    public Guid PortfolioTokenId { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public float PercentChange { get; set; }
    public decimal PriceChange { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
}