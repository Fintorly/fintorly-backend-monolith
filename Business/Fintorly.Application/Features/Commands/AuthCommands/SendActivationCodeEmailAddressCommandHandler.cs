namespace Fintorly.Application.Features.Commands.AuthCommands;

public class SendActivationCodeEmailAddressCommandHandler : IRequestHandler<SendActivationCodeEmailAddressCommand, IResult>
{
    private IUserAuthRepository _authRepository;

    public SendActivationCodeEmailAddressCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(SendActivationCodeEmailAddressCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.SendActivationCodeEmailAsync(request);
    }
}