using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.MessageCommands;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, IResult>
{
    private IMessageRepository _messageRepository;
    private IMapper _mapper;

    public CreateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = _mapper.Map<Message>(request);
        var result = await _messageRepository.AddAsync(message);
        if (result)
            return await Result.SuccessAsync();
        return await Result.FailAsync();
    }
}