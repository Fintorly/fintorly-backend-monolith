using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class Question : BaseEntity, IEntity
{
    //Soru içeriği
    public string Content { get; set; }

    //Soru Tipi
    public QuestionType QuestionType { get; set; }

    //Şıklar ve seçebileceği cevapları
    public ICollection<Choice> Choices { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public Question() => Id = Guid.NewGuid();
}