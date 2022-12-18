namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class LoginWithMailCommand : IRequest<IResult>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}

