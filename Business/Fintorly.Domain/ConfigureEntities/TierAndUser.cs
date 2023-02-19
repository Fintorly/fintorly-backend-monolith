using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

//bir paketi birden fazla kullanıcı alabilir.
//bir kullanıcı birden fazla paket alabilir.
public class TierAndUser : IEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid TierId { get; set; }
    public Tier Tier { get; set; }
}