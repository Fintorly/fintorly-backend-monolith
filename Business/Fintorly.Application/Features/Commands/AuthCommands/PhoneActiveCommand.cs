namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class PhoneActiveCommand : IRequest<IResult>
    {
        public string PhoneNumber { get; set; }
        public string ActivationCode { get; set; }
    }
}

