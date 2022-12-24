namespace Fintorly.Application.Features.Commands.AuthCommands;

public class SendActivationCodePhoneNumberCommandHandler : IRequestHandler<SendActivationCodePhoneNumberCommand, IResult>
{
    private IUserAuthRepository _authRepository;

    public SendActivationCodePhoneNumberCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(SendActivationCodePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.SendActivationCodePhoneAsync(request);
    }
}