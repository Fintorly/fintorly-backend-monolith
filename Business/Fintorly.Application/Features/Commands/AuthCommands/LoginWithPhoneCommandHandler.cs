namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithPhoneCommandHandler : IRequestHandler<LoginWithPhoneCommand,IResult>
{
    private IAuthRepository _authRepository;

    public LoginWithPhoneCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(LoginWithPhoneCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithPhoneAsync(request);
    }
}