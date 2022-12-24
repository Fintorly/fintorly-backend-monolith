using Fintorly.Application.Dtos.UserDtos;

namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class LoginWithUserNameCommand : IRequest<IResult<UserAndTokenDto>>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

