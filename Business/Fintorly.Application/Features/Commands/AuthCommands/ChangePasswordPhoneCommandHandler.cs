namespace Fintorly.Application.Features.Commands.AuthCommands;

public class ChangePasswordPhoneCommandHandler:IRequestHandler<ChangePasswordPhoneCommand,IResult>
{
    private IUserAuthRepository _authRepository;

    public ChangePasswordPhoneCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(ChangePasswordPhoneCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ForgotPasswordPhoneAsync(request);
    }
}