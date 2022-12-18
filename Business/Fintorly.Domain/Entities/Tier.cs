using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities
{
    public class Tier : BaseEntity, IEntity
    {
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public double PointAverage { get; set; }
        public int TotalPoint { get; set; }
        public int TotalVote { get; set; }
        public bool IsChatable { get; set; }
        public PackageType PackageType { get; set; }
        //Bu pakete sahip olan kullanıcılar
        public ICollection<TierAndUser> TierAndUsers { get; set; }

        public Tier() => Id = Guid.NewGuid();
    }
}