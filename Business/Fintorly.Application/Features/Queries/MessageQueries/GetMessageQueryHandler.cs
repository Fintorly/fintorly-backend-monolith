using Fintorly.Application.Dtos.MessageDtos;

namespace Fintorly.Application.Features.Queries.MessageQueries;

public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, IResult<MessageDto>>
{
    private IMessageRepository _messageRepository;
    private IMapper _mapper;

    public GetMessageQueryHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<IResult<MessageDto>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId);
        if (message == null)
            return Result<MessageDto>.Fail();
        var messageDto = _mapper.Map<MessageDto>(message);
        return Result<MessageDto>.Success(messageDto);
    }
}