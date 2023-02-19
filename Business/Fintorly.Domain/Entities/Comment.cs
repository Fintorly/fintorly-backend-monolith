using System;
using System.Collections.Generic;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities
{
    //Mentorün Yorumları
    public class Comment : BaseEntity, IEntity
    {
        public string Content { get; set; }
        public int North { get; set; }
        public int South { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        //Yorumu yapan mentor id
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }
        //mentor'un yorum yaptığı token id
        public Guid? TokenId { get; set; }
        public Token? Token { get; set; }
        //Yoruma gelen şikayetler
        public ICollection<Report> Reports { get; set; }
        public Comment() => Id = Guid.NewGuid();
    }
}