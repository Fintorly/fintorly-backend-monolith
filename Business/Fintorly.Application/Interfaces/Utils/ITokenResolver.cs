using Fintorly.Application.Dtos.AuthDtos;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Utils;

public interface ITokenResolver
{
    Task<AccessTokenInfo> GetTokenInfoAsync();
    public Task<string> GetIdAsync();
    public Task<string> GetEmailAddressAsync();
    public Task<string> GetPhoneAsync();
    public Task<bool> GetIsDeletedAsync();
    public Task<bool> GetIsMentorAsync();
    public Task<string> GetUserNameAsync();
    public Task<string> GetIpAddressAsync();
    public Task<List<OperationClaim>> GetOperationClaimsAsync();
}