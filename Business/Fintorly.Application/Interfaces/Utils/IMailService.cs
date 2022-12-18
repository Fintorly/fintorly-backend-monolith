using Fintorly.Application.Features.Commands.EmailCommands;

namespace Fintorly.Application.Interfaces.Utils
{
    public interface IMailService
    {
        Task<IResult> SendEmail(EmailSendCommand emailSendDto);
        Task<IResult> SendLandingEmail(LandingEmailCommand landingEmailDto);
    }
}

