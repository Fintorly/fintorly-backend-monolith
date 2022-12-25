using Fintorly.Domain.Enums;
using MailKit.Net.Imap;

namespace Fintorly.Application.Features.Commands.AnswerCommands;

public class CreateAnswerMultipleCommand : IRequest<IResult>
{
    public List<CreateAnswer> Answers { get; set; }
}

public class CreateAnswer
{
    public Guid QuestionId { get; set; }
    public QuestionChoice Choice { get; set; }
    public string? Content { get; set; }
}