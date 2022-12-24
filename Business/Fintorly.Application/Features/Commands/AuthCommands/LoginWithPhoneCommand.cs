using System;
using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class LoginWithPhoneCommand : IRequest<IResult<UserAndTokenDto>>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}

