using System;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Utils
{
    public interface IJwtHelper
    {
        Task<AccessToken> CreateTokenAsync(User user, IEnumerable<OperationClaim> operationClaims);
        Task<AccessToken> CreateTokenAsync(Mentor mentor, IEnumerable<OperationClaim> operationClaims);
    }
}

