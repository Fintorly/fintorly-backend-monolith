using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

public class MentorAndUser : IEntity
{
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}