using Fintorly.Domain.Entities;

namespace Fintorly.Application.Dtos.UserDtos
{
    public class UserAndTokenDto
    {
        public Guid TokenId { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }
}

