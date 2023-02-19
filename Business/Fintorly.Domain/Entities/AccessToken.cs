using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class AccessToken : BaseEntity, IEntity
{
    public string Token { get; set; }
    public bool IsValid { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public Guid? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
}