using System;
using Fintorly.Application.Features.Commands.QuestionCommands;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Repositories
{
    public interface IQuestionRepository:IGenericRepository<Question>
    {
	    Task<IResult> AddOrUpdateQuestionAsync(AddOrUpdateQuestionCommand request);
    }
}

