using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class AdditionalFeature : BaseEntity, IEntity
{
    public Guid? PostId { get; set; }
    public Post? Post { get; set; }
    public Guid? TierId { get; set; }
    public Tier? Tier { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public AdditionalFeature() => Id = Guid.NewGuid();
}