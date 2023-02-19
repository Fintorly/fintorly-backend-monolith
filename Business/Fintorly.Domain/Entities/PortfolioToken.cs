using System;
using System.Collections.Generic;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class PortfolioToken : BaseEntity, IEntity
{
    public Guid PortfolioId { get; set; }
    public Portfolio Portfolio { get; set; }
    public string Symbol { get; set; }
    public string BaseAsset { get; set; }
    public string BaseAssetName { get; set; }
    public float Amount { get; set; }
    public decimal Value { get; set; }
    public decimal PriceDiffPercentChange { get; set; }
    public decimal PriceChangeDiff { get; set; }
    public decimal LastPrice { get; set; }
    public ICollection<PortfolioTransaction> PortfolioTransactions { get; set; }
}