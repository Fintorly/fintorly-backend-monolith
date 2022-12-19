namespace Fintorly.Application.Features.Commands.AuthCommands;

public class UserEmailActiveCommandHandler:IRequestHandler<UserEmailActiveCommand,IResult>
{
    private IAuthRepository _authRepository;

    public UserEmailActiveCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(UserEmailActiveCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ActiveEmailByActivationCodeAsync(request);
    }
}