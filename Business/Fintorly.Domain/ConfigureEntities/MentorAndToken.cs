using System;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities
{
	//Bir mentor birden fazla token'a yorum yapabilir
	//Bir token'a birden fazla mentor yorum yapabilir
	public class MentorAndToken: IEntity
	{
		public Guid MentorId { get; set; }
		public Mentor Mentor { get; set; }
		public Guid TokenId { get; set; }
		public Token Token { get; set; }
	}
}

