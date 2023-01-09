using System.Security.Claims;
using Fintorly.Application.Dtos.AuthDtos;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Fintorly.Infrastructure.Utilities.Services;

public class TokenResolver : ITokenResolver
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenResolver(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<string> GetIdAsync()
    {
        var id = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)
            .Value;
        return await Task.FromResult(id);
    }

    public async Task<string> GetEmailAddressAsync()
    {
        var emailAddress = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)
            .Value;
        return await Task.FromResult(emailAddress);
    }

    public async Task<string> GetPhoneAsync()
    {
        var phoneNumber = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.MobilePhone)
            .Value;
        return await Task.FromResult(phoneNumber);
    }

    public async Task<bool> GetIsDeletedAsync()
    {
        var isDeleted = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "IsDeleted")
            .Value;
        if (!string.IsNullOrEmpty(isDeleted))
            return await Task.FromResult(Convert.ToBoolean(isDeleted));
        else
            throw new Exception("IsDeleted alanı ITokenResolver tarafından çözülemedi");
    }

    public async Task<bool> GetIsMentorAsync()
    {
        var isMentor = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "IsMentor")
            .Value;
        if (!string.IsNullOrEmpty(isMentor))
            return await Task.FromResult(Convert.ToBoolean(isMentor));
        else
            throw new Exception("IsMentor alanı ITokenResolver tarafından çözülemedi");
    }

    public async Task<string> GetUserNameAsync()
    {
        var userName = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)
            .Value;
        return await Task.FromResult(userName);
    }

    public async Task<string> GetIpAddressAsync()
    {
        var ipAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        if (string.IsNullOrEmpty(ipAddress))
            return await Task.FromResult(ipAddress);
        throw new Exception("IpAdresi alınamadı.");
    }

    public async Task<List<OperationClaim>> GetOperationClaimsAsync()
    {
        var operationClaims = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "OperationClaims");
        if (operationClaims != null)
            return JsonConvert.DeserializeObject<List<OperationClaim>>(operationClaims.Value)!;
        return null;
    }

    public async Task<AccessTokenInfo> GetTokenInfoAsync()
    {
        var claims = _contextAccessor.HttpContext.User.Claims;
        AccessTokenInfo accessTokenInfo = new AccessTokenInfo();
        accessTokenInfo.Id = claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
        accessTokenInfo.EmailAddress = claims.FirstOrDefault(a => a.Type == ClaimTypes.Email).Value;
        accessTokenInfo.PhoneNumber = claims.FirstOrDefault(a => a.Type == ClaimTypes.MobilePhone).Value;
        accessTokenInfo.IsDeleted = Convert.ToBoolean(claims.FirstOrDefault(a => a.Type == "IsDeleted").Value);
        accessTokenInfo.IsMentor =
            Convert.ToBoolean(claims.FirstOrDefault(a => a.Type == "IsMentor").Value);
        accessTokenInfo.UserName = claims.FirstOrDefault(a => a.Type == ClaimTypes.Name).Value;
        accessTokenInfo.IpAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var operationClaims = claims.FirstOrDefault(a => a.Type == "OperationClaims");
        if (operationClaims == null)
            return null;
        accessTokenInfo.OperationClaims =
            JsonConvert.DeserializeObject<List<OperationClaim>>(operationClaims.Value)!;
        return await Task.FromResult(accessTokenInfo);
    }
}