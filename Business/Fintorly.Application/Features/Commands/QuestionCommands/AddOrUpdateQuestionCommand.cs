using System;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.QuestionCommands
{
    public class AddOrUpdateQuestionCommand : IRequest<IResult>
    {
        public Guid QuestionId { get; set; }
        public string? Content { get; set; }
        public QuestionChoice Key { get; set; }
        public string Value { get; set; }
    }
}