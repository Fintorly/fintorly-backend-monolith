using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Entities
{
    public class ProfilePicture : BaseEntity, IEntity
    {
        public string Title { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public UserType UserType { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Mentor>? Mentors { get; set; }
    }
}

