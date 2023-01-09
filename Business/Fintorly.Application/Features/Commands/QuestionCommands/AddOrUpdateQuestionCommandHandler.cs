namespace Fintorly.Application.Features.Commands.QuestionCommands;

public class AddOrUpdateQuestionCommandHandler : IRequestHandler<AddOrUpdateQuestionCommand, IResult>
{
    private IQuestionRepository _questionRepository;

    public AddOrUpdateQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IResult> Handle(AddOrUpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        return await _questionRepository.AddOrUpdateQuestionAsync(request);
    }
}