using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class Analysis : BaseEntity, IEntity
{
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public int TotalNorth { get; set; }
    public int TotalSouth { get; set; }
    public double AveragePoint { get; set; }
    public int TotalPost { get; set; }
    public int TotalReports { get; set; }
    public double MentorSuccessScore { get; set; }
    public Analysis() => Id = Guid.NewGuid();

}