using Fintorly.Application.Dtos.MentorDtos;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Application.Features.Queries.AuthQueries;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Repositories;

public interface IMentorAuthRepository:IGenericRepository<Mentor>
{
    Task<IEnumerable<OperationClaim>> GetClaimsAsync(Mentor mentor);
    Task<AccessToken> CreateAccessTokenAsync(Mentor mentor);
    Task<IResult<UserAndTokenDto>> RegisterAsync(RegisterCommand registerCommand);
    Task<IResult<UserAndTokenDto>> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand);
    Task<IResult<UserAndTokenDto>> LoginWithEmailAsync(LoginWithMailCommand loginWithMailCommand);
    Task<IResult<UserAndTokenDto>> LoginWithUserNameAsync(LoginWithUserNameCommand loginWithUserNameCommand);
    Task<IResult> ChangePasswordAsync(ChangePasswordCommand changePasswordCommand);
    Task<IResult> ForgotPasswordEmailAsync(ForgotPasswordEmailCommand changePasswordEmailCommand);
    Task<IResult> ForgotPasswordPhoneAsync(ForgotPasswordPhoneCommand changePasswordPhoneCommand);
    Task<IResult> CheckCodeIsTrueByPhoneAsync(CheckCodeIsTrueByPhoneNumberQuery codeIsTrueByPhoneNumberQuery);
    Task<IResult> CheckCodeIsTrueByEmailAsync(CheckCodeIsTrueByEmailAddressQuery codeIsTrueByEmailAddressQuery);
}