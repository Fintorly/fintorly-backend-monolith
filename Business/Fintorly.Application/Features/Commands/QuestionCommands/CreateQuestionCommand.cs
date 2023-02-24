using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;
using MailKit.Net.Imap;

namespace Fintorly.Application.Features.Commands.QuestionCommands;

public class CreateQuestionCommand : IRequest<IResult>
{
    public string Content { get; set; }
    public Dictionary<QuestionChoice, string> Choices { get; set; }
}

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, IResult>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = _mapper.Map<Question>(request);
        var result = await _questionRepository.AddAsync(question);
        if (result)
            return await Result.SuccessAsync();
        return await Result.FailAsync();
    }
}