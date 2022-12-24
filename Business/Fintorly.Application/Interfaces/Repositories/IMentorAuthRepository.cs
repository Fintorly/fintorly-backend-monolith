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
    Task<IResult> RegisterAsync(RegisterCommand registerCommand);
    Task<IResult<MentorAndTokenDto>> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand);
    Task<IResult<MentorAndTokenDto>> LoginWithEmailAsync(LoginWithMailCommand loginWithMailCommand);
    Task<IResult<MentorAndTokenDto>> LoginWithUserNameAsync(LoginWithUserNameCommand loginWithUserNameCommand);
    Task<IResult> ActiveEmailByActivationCodeAsync(EmailActiveCommand emailActiveCommand);
    Task<IResult> ActivePhoneByActivationCodeAsync(PhoneActiveCommand phoneActiveCommand);
    Task<IResult> SendActivationCodeEmailAsync(SendActivationCodeEmailAddressCommand activationCodeEmailAddressCommand );
    Task<IResult> SendActivationCodePhoneAsync(SendActivationCodePhoneNumberCommand activationCodePhoneNumberCommand);
    Task<IResult> ChangePasswordAsync(ChangePasswordCommand changePasswordCommand);
    Task<IResult> ForgotPasswordEmailAsync(ChangePasswordEmailCommand changePasswordEmailCommand);
    Task<IResult> ForgotPasswordPhoneAsync(ChangePasswordPhoneCommand changePasswordPhoneCommand);
    Task<IResult> CheckCodeIsTrueByPhoneAsync(CheckCodeIsTrueByPhoneNumberQuery codeIsTrueByPhoneNumberQuery);
    Task<IResult> CheckCodeIsTrueByEmailAsync(CheckCodeIsTrueByEmailAddressQuery codeIsTrueByEmailAddressQuery);
    Task<IResult> VerificationCodeAddAsync(VerificationCodeAddCommand verificationCodeAddCommand);
}