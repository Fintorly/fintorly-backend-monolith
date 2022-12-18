namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class LoginWithUserNameCommand : IRequest<IResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

