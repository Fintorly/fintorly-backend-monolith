using System;
namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class LoginWithPhoneCommand : IRequest<IResult>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}

