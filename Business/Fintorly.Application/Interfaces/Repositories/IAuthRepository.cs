using System;
using Fintorly.Domain.Entities;
using Fintorly.Application.Features.Commands.AuthCommands;

namespace Fintorly.Application.Interfaces.Repositories
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<IEnumerable<OperationClaim>> GetClaimsAsync(User user);
        Task<AccessToken> CreateAccessTokenAsync(User user, bool isRefresh);
        Task<IResult> CreateAccessTokenByUserIdAsync(Guid userId, bool isRefresh);
        Task<IResult> RegisterAsync(RegisterCommand registerCommand);
        Task<IResult> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand);
        Task<IResult> LoginWithEmailAsync(LoginWithMailCommand loginWithMailCommand);
        Task<IResult> LoginWithUserNameAsync(LoginWithUserNameCommand loginWithUserNameCommand);
        Task<IResult> ActiveEmailByActivationCodeAsync(UserEmailActiveCommand userEmailActiveCommand);
        Task<IResult> ActivePhoneByActivationCodeAsync(UserPhoneActiveCommand userPhoneActiveCommand);
        Task<IResult> SendActivationCodeEmailAsync(string emailAddress);
        Task<IResult> SendActivationCodePhoneAsync(string phoneNumber);
        Task<IResult> ForgotPasswordEmailAsync(UserChangePasswordEmailCommand userChangePasswordEmailCommand);
        Task<IResult> ForgotPasswordPhoneAsync(UserChangePasswordPhoneCommand userChangePasswordPhoneCommand);
        Task<IResult> UpdatePasswordAsync(UserChangePasswordCommand userChangePasswordCommand);
        Task<IResult> CheckCodeIsTrueByPhoneAsync(string phoneNumber, string code);
        Task<IResult> CheckCodeIsTrueByEmailAsync(string emailAddress, string code);
        Task<IResult> VerificationCodeAddAsync(string phoneNumber, string emailAddress);
    }
}

