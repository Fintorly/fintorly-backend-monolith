namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class EmailActiveCommand : IRequest<IResult>
    {
        public string EmailAddress { get; set; }
        public string ActivationCode { get; set; }
    }
}

