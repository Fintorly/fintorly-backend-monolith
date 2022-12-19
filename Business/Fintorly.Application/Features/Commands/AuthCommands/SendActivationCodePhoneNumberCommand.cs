namespace Fintorly.Application.Features.Commands.AuthCommands;

public class SendActivationCodePhoneNumberCommand:IRequest<IResult>
{
    public string PhoneNumber { get; set; }
}