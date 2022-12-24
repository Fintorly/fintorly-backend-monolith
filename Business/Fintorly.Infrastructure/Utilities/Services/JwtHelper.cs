using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Fintorly.Application.Extensions;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Options;
using Fintorly.Infrastructure.Utilities.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Fintorly.Infrastructure.Utilities.Services
{
    public class JwtHelper : IJwtHelper
    {
        IMapper _mapper;
        IConfiguration Configuration { get; }
        TokenOptions _tokenOptions;
        public JwtHelper(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _mapper = mapper;
        }

        public Task<AccessToken> CreateTokenAsync(User user, IEnumerable<OperationClaim> operationClaims,bool isMentor)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(user, signingCredentials, operationClaims,isMentor);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var accessToken = new AccessToken()
            {
                Token = token,
                User = user,
                UserId = user.Id
            };
            return Task.FromResult(accessToken);
        }

        public Task<AccessToken> CreateTokenAsync(Mentor mentor, IEnumerable<OperationClaim> operationClaims,bool isMentor)
        {
            var user = _mapper.Map<User>(mentor);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(user, signingCredentials, operationClaims,isMentor);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var accessToken = new AccessToken()
            {
                Token = token,
                Mentor = mentor,
                MentorId = mentor.Id
            };
            return Task.FromResult(accessToken);
        }

        public JwtSecurityToken CreateJwtSecurityToken(User user, SigningCredentials signingCredentials, IEnumerable<OperationClaim> operationClaims,bool isMentor)
        {
            var jwt = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims,isMentor),
                signingCredentials: signingCredentials
            );
            return jwt;
        }
        public IEnumerable<Claim> SetClaims(User user, IEnumerable<OperationClaim> operationClaims,bool isMentor)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.EmailAddress);
            claims.AddName(user.UserName);
            claims.AddPhone(user.PhoneNumber);
            claims.AddIpAddress(user.IpAddress!);
            claims.AddIsDeletedStatus(user.IsDeleted);
            claims.AddIsMentor(isMentor);
            claims.AddRoles(operationClaims.Select(a => a.Name).ToArray());
            return claims;
        }
    }
}

