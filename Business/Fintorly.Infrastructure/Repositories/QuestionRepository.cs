using System;
using Fintorly.Application.Features.Commands.QuestionCommands;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private FintorlyContext _context;
        public QuestionRepository(FintorlyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IResult> UpdateQuestionAsync(UpdateQuestionCommand request)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(a => a.Id == request.QuestionId);
            if (question is null)
                return Result.Fail();
            foreach (var choice in request.Choices)
            {
                if (question.Choices.TryGetValue(choice.Key, out var value))
                {
                    value = choice.Value;
                }
                else
                {
                    question.Choices.Add(choice.Key,choice.Value);
                }

            }
            _context.Update(question);
            await _context.SaveEntitiesAsync();
            return Result.Success();
        }
    }
}

