namespace Fintorly.Application.Interfaces.Utils
{
    public interface IPhoneService
    {
        Task<IResult> SendPhoneVerificationCodeAsync(string phoneNumber, string verificationCode);
        Task<IResult> SendPhoneContentAsync(string phoneNumber, string verificationCode);
    }
}

