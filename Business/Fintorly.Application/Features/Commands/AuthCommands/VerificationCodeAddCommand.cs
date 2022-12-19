namespace Fintorly.Application.Features.Commands.AuthCommands;

public class VerificationCodeAddCommand:IRequest<IResult>
{
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}