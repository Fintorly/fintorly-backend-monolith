using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Domain.Entities;

public class Message : BaseEntity, IEntity
{
    public string Content { get; set; }
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    public ICollection<MessagePicture> MessagePictures { get; set; }    
    public ICollection<MessageAndReaction> MessageAndReactions { get; set; }
    public Message() => Id = Guid.NewGuid();
}