using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities;

public class Connection : BaseEntity, IEntity
{
    public string ConnectionId { get; set; }
    public bool IsConnected { get; set; }
    public DateTime ConnectionStartDate { get; set; }
    public DateTime ConnectionEndDate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Connection() => Id = Guid.NewGuid();
}