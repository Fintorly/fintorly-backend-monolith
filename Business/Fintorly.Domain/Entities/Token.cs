using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Domain.Entities
{
    public class Token:BaseEntity,IEntity
	{
		public string Name { get; set; }
		public string Url { get; set; }
        //Tokenların yorumu olacak.
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserAndToken> InterestedTokens { get; set; }
        public ICollection<MentorAndToken> MentorAndTokens { get; set; }
        public Token() => Id = Guid.NewGuid();
	}
}
