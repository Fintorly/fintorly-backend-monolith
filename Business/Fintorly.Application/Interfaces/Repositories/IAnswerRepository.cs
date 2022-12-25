using Fintorly.Application.Features.Commands.AnswerCommands;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Repositories
{
    public interface IAnswerRepository : IGenericRepository<Answer>
    {
	    Task<bool> AddMultipleAnswer(CreateAnswerMultipleCommand command);
    }
}

