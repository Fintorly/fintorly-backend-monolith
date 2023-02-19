using System;
using System.Collections.Generic;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class Post : BaseEntity, IEntity
{
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public string Content { get; set; }
    public int TotalNorth { get; set; }
    public int TotalSouth { get; set; }
    public double AveragePoint { get; set; }

    //Pakete göre gönderi yayınlama
    public Guid TierId { get; set; }
    public Tier Tier { get; set; }
    public ICollection<Report>? Reports { get; set; }
    public ICollection<AdditionalFeature>? AdditionalFeatures { get; set; }
    public ICollection<PostPicture> PostPictures { get; set; }
    public Post() => Id = Guid.NewGuid();
}