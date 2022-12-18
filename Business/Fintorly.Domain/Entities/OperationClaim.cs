using System;
using System.Reflection.PortableExecutable;
using Fintorly.Domain.Common;
using Fintorly.Domain.ConfigureEntities;

namespace Fintorly.Domain.Entities
{
    public class OperationClaim : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public ICollection<UserAndOperationClaim> UserAndOperationClaims { get; set; }

        public OperationClaim() => Id = Guid.NewGuid();
    }
}