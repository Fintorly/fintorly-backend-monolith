using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class Question: BaseEntity,IEntity
{
    //Şıklar ve seçebileceği cevapları
    public Dictionary<QuestionChoice,string> Choices{ get; set; }
    public ICollection<Answer> Answers { get; set; }
    public Question() => Id = Guid.NewGuid();
}