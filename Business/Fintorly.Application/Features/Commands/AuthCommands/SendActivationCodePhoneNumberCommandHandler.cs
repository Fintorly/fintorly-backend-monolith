namespace Fintorly.Application.Features.Commands.AuthCommands;

public class SendActivationCodePhoneNumberCommandHandler : IRequestHandler<SendActivationCodePhoneNumberCommand, IResult>
{
    private IAuthRepository _authRepository;

    public SendActivationCodePhoneNumberCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(SendActivationCodePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.SendActivationCodePhoneAsync(request);
    }
}