using System;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

public class UserAndCategory
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}