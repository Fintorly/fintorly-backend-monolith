using Fintorly.Application.Features.Commands.AnswerCommands;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        private FintorlyContext _context;

        public AnswerRepository(FintorlyContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<bool> AddAsync(Answer entity)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(a => a.Id == entity.QuestionId);
            if (question is null)
                return false;
            entity.Question = question;
            question.Answers.Add(entity);
            _context.Answers.AddRange(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddMultipleAnswer(CreateAnswerMultipleCommand command)
        {
            foreach (var answer in command.Answers)
            {
                var question = await _context.Questions.SingleOrDefaultAsync(a => a.Id == answer.QuestionId);
                if (question is null)
                    continue;
                var newAnswer = new Answer()
                {
                    Choice = answer.Choice,
                    Content = answer.Content,
                    Question = question,
                    QuestionId = question.Id,
                };
                question.Answers.Add(newAnswer);
                await _context.Answers.AddAsync(newAnswer);
                _context.Update(question);
            }
            await _context.SaveChangesAsync();

            return true;
        }
    }
}