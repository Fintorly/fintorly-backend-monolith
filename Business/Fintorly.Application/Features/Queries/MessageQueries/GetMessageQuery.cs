using Fintorly.Application.Dtos.MessageDtos;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Queries.MessageQueries;

public class GetMessageQuery : IRequest<IResult<MessageDto>>
{
    public Guid MessageId { get; set; }
}