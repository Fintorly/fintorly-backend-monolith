using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.AnswerCommands;

public class CreateAnswerMultipleCommandHandler : IRequestHandler<CreateAnswerMultipleCommand, IResult>
{
    private readonly IAnswerRepository _answerRepository;

    public CreateAnswerMultipleCommandHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<IResult> Handle(CreateAnswerMultipleCommand request, CancellationToken cancellationToken)
    {
        var result=await _answerRepository.AddMultipleAnswer(request);
        if (result)
            return Result.Success();
        return Result.Fail();
    }
}