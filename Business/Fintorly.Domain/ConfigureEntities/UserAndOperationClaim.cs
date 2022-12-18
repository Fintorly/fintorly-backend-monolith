using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;

namespace Fintorly.Domain.ConfigureEntities
{
    public class UserAndOperationClaim : IEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}