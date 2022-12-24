using Fintorly.Application.Dtos.PortfolioTransactionDtos;

namespace Fintorly.Application.Dtos.PortfolioTokenDtos;

public class PortfolioTokenDto
{
    public Guid PortfolioId { get; set; }
    public string Symbol { get; set; }
    public string BaseAsset { get; set; }
    public string BaseAssetName { get; set; }
    public float Amount { get; set; }
    public decimal Value { get; set; }
    public decimal PriceDiffPercentChange { get; set; }
    public decimal PriceChangeDiff { get; set; }
    public decimal LastPrice { get; set; }
    public ICollection<PortfolioTransactionDto> PortfolioTransactions { get; set; }
}