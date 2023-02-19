using System;
using System.Collections.Generic;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities
{
    public class Tier : BaseEntity, IEntity
    {
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public double PointAverage { get; set; }
        public int TotalPoint { get; set; }
        public int TotalVote { get; set; }

        public PackageType PackageType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        //Ek özellikler
        public ICollection<AdditionalFeature> AdditionalFeatures { get; set; }
        //Bu paket'i satın alan kullanıcıların görebilmesi için postları burada tutuyoruz.
        public ICollection<Post> Posts { get; set; }
        //Bu pakete sahip olan kullanıcılar
        public ICollection<TierAndUser> TierAndUsers { get; set; }
        public Tier() => Id = Guid.NewGuid();
    }
}