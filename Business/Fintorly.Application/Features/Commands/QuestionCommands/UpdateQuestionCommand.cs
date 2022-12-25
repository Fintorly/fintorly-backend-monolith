using System;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.QuestionCommands
{
    public class UpdateQuestionCommand : IRequest<IResult>
    {
        public Guid QuestionId { get; set; }
        public Dictionary<QuestionChoice, string> Choices { get; set; }
    }
}