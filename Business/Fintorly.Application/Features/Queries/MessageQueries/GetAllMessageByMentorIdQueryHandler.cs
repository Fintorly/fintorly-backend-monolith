using Fintorly.Application.Dtos.MessageDtos;

namespace Fintorly.Application.Features.Queries.MessageQueries;

public class
    GetAllMessageByMentorIdQueryHandler : IRequestHandler<GetAllMessageByMentorIdQuery, IResult<List<MessageDto>>>
{
    private IMessageRepository _messageRepository;
    private IMapper _mapper;
    public GetAllMessageByMentorIdQueryHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<IResult<List<MessageDto>>> Handle(GetAllMessageByMentorIdQuery request,
        CancellationToken cancellationToken)
    {
        var result= await _messageRepository.GetAllMessageByMentorIdAsync(request.MentorId,cancellationToken);
        if (result == null)
            return await Result<List<MessageDto>>.FailAsync();
        var messages = _mapper.Map<List<MessageDto>>(result);
        return await Result<List<MessageDto>>.SuccessAsync(messages);
    }
}