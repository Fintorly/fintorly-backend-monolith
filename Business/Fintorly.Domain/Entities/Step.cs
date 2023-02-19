using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class Step : BaseEntity, IEntity
{
    public double NextSegmentRequiredPoint { get; set; }
    public double CurrentPoint { get; set; }
    public int TotalPost { get; set; }
    public Segment Segment { get; set; }
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Step() => Id = Guid.NewGuid();
}