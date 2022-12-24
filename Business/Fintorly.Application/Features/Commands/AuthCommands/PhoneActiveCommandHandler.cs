namespace Fintorly.Application.Features.Commands.AuthCommands;

public class PhoneActiveCommandHandler : IRequestHandler<PhoneActiveCommand, IResult>
{
    private IUserAuthRepository _authRepository;

    public PhoneActiveCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(PhoneActiveCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ActivePhoneByActivationCodeAsync(request);
    }
}