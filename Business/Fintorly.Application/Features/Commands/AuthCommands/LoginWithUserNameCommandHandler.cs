namespace Fintorly.Application.Features.Commands.AuthCommands;

public class LoginWithUserNameCommandHandler : IRequestHandler<LoginWithUserNameCommand,IResult>
{
    private IAuthRepository _authRepository;

    public LoginWithUserNameCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(LoginWithUserNameCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginWithUserNameAsync(request);
    }
}