using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.AnswerCommands;

public class CreateAnswerMultipleCommandHandler : IRequestHandler<CreateAnswerMultipleCommand, IResult>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public CreateAnswerMultipleCommandHandler(IAnswerRepository answerRepository, IMapper mapper)
    {
        _answerRepository = answerRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(CreateAnswerMultipleCommand request, CancellationToken cancellationToken)
    {
        
        var result=await _answerRepository.AddMultipleAnswer(request);
        if (result)
            return Result.Success();
        return Result.Fail();
    }
}