using Fintorly.Application.Dtos.MessageDtos;

namespace Fintorly.Application.Features.Queries.MessageQueries;

public class GetAllMessageByGroupIdQuery : IRequest<IResult<List<MessageDto>>>
{
    public Guid GroupId { get; set; }
}