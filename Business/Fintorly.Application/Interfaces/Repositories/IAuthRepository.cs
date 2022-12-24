﻿using System;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Entities;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Application.Features.Queries.AuthQueries;

namespace Fintorly.Application.Interfaces.Repositories
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<IEnumerable<OperationClaim>> GetClaimsAsync(User user);
        Task<AccessToken> CreateAccessTokenAsync(User user);
        Task<AccessToken> CreateAccessTokenAsync(Mentor mentor);
        Task<IResult> RegisterAsync(RegisterCommand registerCommand);
        Task<IResult<UserAndTokenDto>> LoginWithPhoneAsync(LoginWithPhoneCommand loginWithPhoneCommand);
        Task<IResult<UserAndTokenDto>> LoginWithEmailAsync(LoginWithMailCommand loginWithMailCommand);
        Task<IResult<UserAndTokenDto>> LoginWithUserNameAsync(LoginWithUserNameCommand loginWithUserNameCommand);
        Task<IResult> ActiveEmailByActivationCodeAsync(UserEmailActiveCommand userEmailActiveCommand);
        Task<IResult> ActivePhoneByActivationCodeAsync(UserPhoneActiveCommand userPhoneActiveCommand);
        Task<IResult> SendActivationCodeEmailAsync(SendActivationCodeEmailAddressCommand activationCodeEmailAddressCommand );
        Task<IResult> SendActivationCodePhoneAsync(SendActivationCodePhoneNumberCommand activationCodePhoneNumberCommand);
        Task<IResult> ChangePasswordAsync(UserChangePasswordCommand userChangePasswordCommand);
        Task<IResult> ForgotPasswordEmailAsync(UserChangePasswordEmailCommand userChangePasswordEmailCommand);
        Task<IResult> ForgotPasswordPhoneAsync(UserChangePasswordPhoneCommand userChangePasswordPhoneCommand);
        Task<IResult> CheckCodeIsTrueByPhoneAsync(CheckCodeIsTrueByPhoneNumberQuery codeIsTrueByPhoneNumberQuery);
        Task<IResult> CheckCodeIsTrueByEmailAsync(CheckCodeIsTrueByEmailAddressQuery codeIsTrueByEmailAddressQuery);
        Task<IResult> VerificationCodeAddAsync(VerificationCodeAddCommand verificationCodeAddCommand);
    }
}

