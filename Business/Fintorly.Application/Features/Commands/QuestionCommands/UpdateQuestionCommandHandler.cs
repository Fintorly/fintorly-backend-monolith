namespace Fintorly.Application.Features.Commands.QuestionCommands;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, IResult>
{
    private IQuestionRepository _questionRepository;

    public UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IResult> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        return await _questionRepository.UpdateQuestionAsync(request);
    }
}