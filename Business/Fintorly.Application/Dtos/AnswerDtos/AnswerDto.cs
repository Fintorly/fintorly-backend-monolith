using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Dtos.AnswerDtos;

public class AnswerDto
{
    public QuestionChoice Choice { get; set; }
    public string Content { get; set; }
    public Guid QuestionId { get; set; }
}