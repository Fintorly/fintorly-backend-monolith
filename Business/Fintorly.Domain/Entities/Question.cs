using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class Question:BaseEntity,IEntity
{
    public string Content { get; set; }
    public ICollection<Answer> Answers { get; set; }
    
    public Question() => Id = Guid.NewGuid();
}