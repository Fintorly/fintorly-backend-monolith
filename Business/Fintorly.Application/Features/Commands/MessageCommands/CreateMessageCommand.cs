namespace Fintorly.Application.Features.Commands.MessageCommands;

public class CreateMessageCommand : IRequest<IResult>
{
    public string Content { get; set; }
    public Guid MentorId { get; set; }
    public Guid GroupId { get; set; }
}