using Fintorly.Domain.Entities;

namespace Fintorly.Application.Dtos.MentorDtos;


public class MentorAndTokenDto
{
    public Guid TokenId { get; set; }
    public Guid MentorId { get; set; }
    public MentorDto Mentor { get; set; }
    public string Token { get; set; }
    public DateTime CreatedDate { get; set; }
    public string IpAddress { get; set; }
}