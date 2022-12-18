using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Domain.Entities;

public class Reaction : BaseEntity, IEntity
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
    public ICollection<MessageAndReaction> MessageAndReactions { get; set; }
    public Reaction() => Id = Guid.NewGuid();
}