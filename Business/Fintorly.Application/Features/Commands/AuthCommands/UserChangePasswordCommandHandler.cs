namespace Fintorly.Application.Features.Commands.AuthCommands;

public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, IResult>
{
    private IAuthRepository _authRepository;

    public UserChangePasswordCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ChangePasswordAsync(request);
    }
}