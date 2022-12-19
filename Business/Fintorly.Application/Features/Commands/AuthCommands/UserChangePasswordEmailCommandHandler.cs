namespace Fintorly.Application.Features.Commands.AuthCommands;

public class  UserChangePasswordEmailCommandHandler:IRequestHandler<UserChangePasswordEmailCommand,IResult>
{
    private IAuthRepository _authRepository;

    public UserChangePasswordEmailCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(UserChangePasswordEmailCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ForgotPasswordEmailAsync(request);
    }
}