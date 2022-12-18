using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities
{
    public class Report : BaseEntity, IEntity
    {
     
        public string Title { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsSolved { get; set; }
        public string AdminNote { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }

        public Report() => Id = Guid.NewGuid();
    }
}