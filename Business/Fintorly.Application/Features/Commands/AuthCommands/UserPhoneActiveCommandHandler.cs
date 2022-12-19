namespace Fintorly.Application.Features.Commands.AuthCommands;

public class UserPhoneActiveCommandHandler:IRequestHandler<UserPhoneActiveCommand,IResult>
{
    private IAuthRepository _authRepository;

    public UserPhoneActiveCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IResult> Handle(UserPhoneActiveCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.ActivePhoneByActivationCodeAsync(request);
    }
}