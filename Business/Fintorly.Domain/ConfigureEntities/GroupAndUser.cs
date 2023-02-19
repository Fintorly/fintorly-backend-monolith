using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

//Bir grupta birden fazla user olabilir
//Bir user birden fazla grupta olabilir
public class GroupAndUser: IEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}