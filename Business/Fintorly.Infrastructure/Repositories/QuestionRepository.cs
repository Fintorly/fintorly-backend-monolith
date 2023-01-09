using System;
using AutoMapper;
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
        private IMapper _mapper;

        public QuestionRepository(FintorlyContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResult> AddOrUpdateQuestionAsync(AddOrUpdateQuestionCommand request)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(a => a.Id == request.QuestionId);
            if (question is null)
            {
                var newQuestion = _mapper.Map<Question>(request);
                var newChoice = new Choice()
                {
                    Key = request.Key,
                    Value = request.Value,
                    Question = newQuestion,
                    QuestionId = newQuestion.Id
                };
                newQuestion.Choices.Add(newChoice);
                await _context.Choices.AddAsync(newChoice);
                await _context.Questions.AddAsync(newQuestion);
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            else
            {
                var choiceExist = question.Choices.SingleOrDefault(a => a.Key == request.Key);
                if (choiceExist is not null)
                {
                    choiceExist.Value = request.Value;
                }
                else
                {
                    var choice = new Choice()
                    {
                        Key = request.Key,
                        Value = request.Value,
                        Question = question,
                        QuestionId = question.Id
                    };
                    question.Choices.Add(choice);
                }
                _context.Update(question);
                await _context.SaveChangesAsync();
                return Result.Success();
            }
        }
    }
}