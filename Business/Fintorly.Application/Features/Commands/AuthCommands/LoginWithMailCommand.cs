using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class LoginWithMailCommand : IRequest<IResult<UserAndTokenDto>>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}

