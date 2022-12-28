using Fintorly.Domain.Entities;

namespace Fintorly.Application.Dtos.MessageDtos;

public class MessageDto
{
    public string Content { get; set; }
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
}