namespace Fintorly.Application.Features.Commands.EmailCommands
{
    public class LandingEmailCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
    }
}

