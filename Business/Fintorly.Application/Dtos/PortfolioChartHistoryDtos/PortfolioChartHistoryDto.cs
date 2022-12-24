namespace Fintorly.Application.Dtos.PortfolioChartHistoryDtos;

public class PortfolioChartHistoryDto
{
    public Guid PortfolioId { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceChange { get; set; }
    public decimal TotalPriceChangePercent { get; set; }
    public decimal TotalPriceUser24Hour { get; set; }
}