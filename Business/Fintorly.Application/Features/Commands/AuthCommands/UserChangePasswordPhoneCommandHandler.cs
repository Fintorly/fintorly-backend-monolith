namespace Fintorly.Application.Features.Commands.AuthCommands;

public class UserChangePasswordPhoneCommandHandler:IRequestHandler<UserChangePasswordPhoneCommand,IResult>
{
    private IAuthRepository _authRepository;

    public UserChangePasswordPhoneCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(UserChangePasswordPhoneCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ForgotPasswordPhoneAsync(request);
    }
}