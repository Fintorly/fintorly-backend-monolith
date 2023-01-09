using Fintorly.Application.Dtos.PortfolioChartHistoryDtos;
using Fintorly.Application.Dtos.PortfolioTokenDtos;

namespace Fintorly.Application.Dtos.PortfolioDtos;

public class PortfolioDto
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public Guid MentorId { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceChange { get; set; }
    public decimal TotalPriceChangePercent { get; set; }
    public decimal TotalPriceUser24Hour { get; set; }
    public ICollection<PortfolioTokenDto> PortfolioTokens { get; set; }
    public ICollection<PortfolioChartHistoryDto> PortfolioChartHistories { get; set; }
}