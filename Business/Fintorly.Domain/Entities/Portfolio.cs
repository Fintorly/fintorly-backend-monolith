using System;
using System.Collections.Generic;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class Portfolio : BaseEntity, IEntity
{
    public string Name { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public Guid? MentorId { get; set; }

    public Mentor? Mentor { get; set; }

    //Ana Fiyat Güncel 
    public decimal TotalPrice { get; set; }

    //Hesaptaki değişim farkı
    public decimal TotalPriceChange { get; set; }

    //Hesaptaki günlük değişim yüzdeliği
    public decimal TotalPriceChangePercent { get; set; }

    //Hesaptaki değişim yüzdeliğini tutmak için güne başladığı fiyat
    public decimal TotalPriceUser24Hour { get; set; }

    public ICollection<PortfolioToken> PortfolioTokens { get; set; }
    public ICollection<PortfolioChartHistory> PortfolioChartHistories { get; set; }
}