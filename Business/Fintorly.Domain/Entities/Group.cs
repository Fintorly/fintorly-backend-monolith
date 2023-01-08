using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Domain.Entities
{
    //mentörün yönettiği grup
    public class Group : BaseEntity, IEntity
    {
        public string Title { get; set; }
        //Grup yöneticisi id
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }
        public Guid TierId { get; set; }
        public Tier Tier { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        //Gruptaki kullanıcılar
        public ICollection<GroupAndUser> GroupAndUsers { get; set; }
        //Gruptaki mesajlar
        public ICollection<Message> Messages { get; set; }
        public Group() => Id = Guid.NewGuid();
    }
    
    
}