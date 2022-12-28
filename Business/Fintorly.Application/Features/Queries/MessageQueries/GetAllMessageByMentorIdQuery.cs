using Fintorly.Application.Dtos.MessageDtos;

namespace Fintorly.Application.Features.Queries.MessageQueries;

public class GetAllMessageByMentorIdQuery : IRequest<IResult<List<MessageDto>>>
{
    public Guid MentorId { get; set; }
}