using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities;

public class Answer : BaseEntity, IEntity
{
    //Sorunun Şıklı cevapları
    public QuestionChoice Choice { get; set; }
    //Kullanıcı QuestionChoice olarak 5'i seçerse girmesi gereken content
    public string Content { get; set; }

    //Cevabın bağlı olduğu QuestionId
    public Guid QuestionId { get; set; }

    public Question Question { get; set; }

    //Cevaplayan kullanıcı Id
    public Guid? UserId { get; set; }

    public User? User { get; set; }

    //Cevaplayan mentor Id
    public Guid? MentorId { get; set; }
    public Mentor? Mentor { get; set; }

    public Answer() => Id = Guid.NewGuid();
}