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
        var answers = _mapper.Map<List<Answer>>(request.Answers);
        var result=await _answerRepository.AddMultipleAnswer(answers);
        if (result)
            return Result.Success();
        return Result.Fail();
    }
}