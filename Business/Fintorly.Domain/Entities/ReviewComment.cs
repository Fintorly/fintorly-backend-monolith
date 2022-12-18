using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities
{
    //Kullanıcı yorumları
    public class ReviewComment : BaseEntity, IEntity
    {
        //Kullanıcı yorumunun içeriği
        public string Content { get; set; }

        //Kullanıcının verdiği puan
        public double Point { get; set; }

        //Yorum yapan kullanıcı id
        public Guid UserId { get; set; }

        public User User { get; set; }

        //Yorum yapılan mentor id
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }

        public ReviewComment() => Id = Guid.NewGuid();
    }
}