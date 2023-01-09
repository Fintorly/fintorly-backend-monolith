using Fintorly.Application.Dtos.MessageDtos;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Repositories;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<List<Message>> GetAllMessageByMentorIdAsync(Guid mentorId,CancellationToken cancellationToken);
    Task<List<Message>> GetAllMessageByGroupIdAsync(Guid groupId,CancellationToken cancellationToken);
}