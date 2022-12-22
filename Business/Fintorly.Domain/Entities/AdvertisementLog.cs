using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class AdvertisementLog:BaseEntity,IEntity
{
    public Guid AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; }
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
}