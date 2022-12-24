namespace Fintorly.Application.Features.Commands.AuthCommands;

public class  ChangePasswordEmailCommandHandler:IRequestHandler<ChangePasswordEmailCommand,IResult>
{
    private IUserAuthRepository _authRepository;

    public ChangePasswordEmailCommandHandler(IUserAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(ChangePasswordEmailCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ForgotPasswordEmailAsync(request);
    }
}