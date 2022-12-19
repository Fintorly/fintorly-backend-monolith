namespace Fintorly.Application.Features.Commands.AuthCommands;

public class SendActivationCodeEmailAddressCommand : IRequest<IResult>
{
    public string EmailAddress { get; set; }
}