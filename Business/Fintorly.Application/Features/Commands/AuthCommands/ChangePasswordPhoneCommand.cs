namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class ChangePasswordPhoneCommand : IRequest<IResult>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ReTypePassword { get; set; }
    }
}

