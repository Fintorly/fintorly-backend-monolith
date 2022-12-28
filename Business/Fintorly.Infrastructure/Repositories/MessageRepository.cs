using Fintorly.Application.Dtos.MessageDtos;
using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    private FintorlyContext _context;

        public MessageRepository(FintorlyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetAllMessageByMentorIdAsync(Guid mentorId, CancellationToken cancellationToken)
    {
        var mentorIsExist = await _context.Mentors.Include(a => a.Messages).SingleOrDefaultAsync(a => a.Id == mentorId);
        if (mentorIsExist == null)
            return null;

        return mentorIsExist.Messages.ToList();
    }

    public async Task<List<Message>> GetAllMessageByGroupIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        var groupIsExist = await _context.Groups.Include(a => a.Messages).SingleOrDefaultAsync(a => a.Id == groupId);
        if (groupIsExist == null)
            return null;

        return groupIsExist.Messages.ToList();
    }
}