namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class UserChangePasswordCommand : IRequest<IResult>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ReTypePassword { get; set; }
    }
}

