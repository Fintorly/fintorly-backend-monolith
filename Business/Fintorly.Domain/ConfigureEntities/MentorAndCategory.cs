using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

public class MentorAndCategory:IEntity
{
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}