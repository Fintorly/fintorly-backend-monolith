using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Application.Features.Queries.AuthQueries;

namespace Fintorly.Application.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<IResult> ActiveEmailByActivationCodeAsync(EmailActiveCommand emailActiveCommand);
    Task<IResult> ActivePhoneByActivationCodeAsync(PhoneActiveCommand phoneActiveCommand);
    Task<IResult> SendActivationCodeEmailAsync(SendActivationCodeEmailAddressCommand activationCodeEmailAddressCommand );
    Task<IResult> SendActivationCodePhoneAsync(SendActivationCodePhoneNumberCommand activationCodePhoneNumberCommand);
    Task<IResult> VerificationCodeAddAsync(VerificationCodeAddCommand verificationCodeAddCommand);
}