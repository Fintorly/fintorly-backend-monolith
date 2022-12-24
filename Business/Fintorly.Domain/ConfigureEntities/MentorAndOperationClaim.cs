using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities;

public class MentorAndOperationClaim : BaseEntity, IEntity
{
    public Guid MentorId { get; set; }
    public Mentor Mentor { get; set; }
    public Guid OperationClaimId { get; set; }
    public OperationClaim OperationClaim { get; set; }
}